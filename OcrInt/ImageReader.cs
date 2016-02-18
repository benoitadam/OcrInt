using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Tesseract;

namespace OcrInt
{
    public class ImageReader : IDisposable
    {
        static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        private TesseractEngine engine;

        /// <summary>
        /// Crée un nouveau moteur de Tesseract
        /// </summary>
        public ImageReader()
        {
            ResetEngine();
        }

        /// <summary>
        /// Libère les ressources
        /// </summary>
        public void Dispose()
        {
            DisposeEngine();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Libère les ressources du moteur de Tesseract
        /// </summary>
        public void DisposeEngine()
        {
            if (engine != null)
            {
                engine.Dispose();
                engine = null;
            }
        }

        /// <summary>
        /// Réinitialise le moteur de Tesseract
        /// </summary>
        public void ResetEngine()
        {
            DisposeEngine();

            log.Debug("Chargement du moteur de Tesseract");
            engine = new TesseractEngine(@"./tessdata", "fra", EngineMode.Default);
        }

        /// <summary>
        /// Lecture du texte de l'image
        /// </summary>
        /// <param name="imagePath">Chemin vers l'image</param>
        /// <returns></returns>
        private string PrivateRead(string imagePath)
        {
            string text;
            float meanConfidence;

            log.Debug("Chargement de l'image '{0}'", imagePath);
            using (var img = Pix.LoadFromFile(imagePath))
            {
                lock (engine)
                {
                    log.Info("Analyse de l'image '{0}' {1}x{2}", imagePath, img.Width, img.Height);
                    using (var page = engine.Process(img))
                    {
                        log.Debug("Lecture du texte");
                        text = page.GetText();
                        if (String.IsNullOrEmpty(text))
                            throw new Exception("text is null or empty");

                        log.Trace("text = '{0}'", text);

                        meanConfidence = page.GetMeanConfidence();
                        log.Info("Taille du texte : '{0}', Taux de confiance : '{1}'", text.Length, meanConfidence);
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Lecture du texte de l'image
        /// </summary>
        /// <param name="imagePath">Chemin vers l'image</param>
        /// <param name="attempt">Nombre de tentatives (en cas d'erreur on recommence le traitement)</param>
        /// <returns></returns>
        public string ReadText(string imagePath, int attempt = 3)
        {
            string text;
            Exception error = null;

            if (!File.Exists(imagePath))
                throw new ArgumentException($"file '{imagePath}' doesn't exists");
            
            var start = DateTime.Now;

            // 3 tentatives
            for (int i = 0; i < attempt; i++)
            {
                try
                {
                    text = PrivateRead(imagePath);

                    // Log le temps de traitement
                    var totalSeconds = (DateTime.Now - start).TotalSeconds;
                    log.Debug("Le temps de traitement est de '{0}' secondes", totalSeconds);

                    return text;
                }
                catch (Exception ex)
                {
                    error = ex;

                    // Reinitialise le moteur en cas d'erreur
                    ResetEngine();
                    
                    // Si le fichier n'est pas une image
                    if (ex is IOException && ex.Message.StartsWith("Failed to load image"))
                        throw new Exception($"Le fichier '{imagePath}' n'est pas une image", ex);

                    log.Error(ex, "attempt : {0} / {1}", i, attempt);
                }
            }
            
            throw error;
        }
    }
}