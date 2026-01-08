using System;
using BerryAIGen.Common;
using System.Diagnostics;
using System.Net;
using System.Security.Policy;
using System.Threading;
using System.Windows;
using BerryAIGen.Civitai;
using BerryAIGen.Civitai.Models;
using BerryAIGen.Toolkit.Models;
using BerryAIGen.Toolkit.Services;

namespace BerryAIGen.Toolkit.Controls
{
    public partial class ThumbnailView
    {
        private async void SearchModel(object obj)
        {
            if (Model.CurrentImage?.ModelHash == null) return;
            
            var hash = Model.CurrentImage.ModelHash;

            using (var client = new CivitaiClient())
            {
                try
                {
                    var modelVersion = await client.GetModelVersionsByHashAsync(hash, CancellationToken.None);

                    Process.Start("explorer.exe", $"\"https://civitai.com/models/{modelVersion.ModelId}?modelVersionId={modelVersion.Id}\"");
                }
                catch (CivitaiRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
                {
                    var message = "The requested model hash was not found";
                    await ServiceLocator.MessageService.Show(message, "Search Model", PopupButtons.OK);
                }
            }
        }
        
    }
}







