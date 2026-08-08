using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SocialNetworkAnalysis.Application.Abstractions.Commands;
using SocialNetworkAnalysis.Application.Abstractions.Queries;
using System.Threading.Tasks;
using SocialNetworkAnalysis.Presentation.Services;

namespace SocialNetworkAnalysis.Presentation.Components.Pages
{
    public partial class ImportGraph
    {
        [Inject]
        private IImportGraphService _importGraphService { get; set; }

        [Inject]
        private IGetWholeGraphService _getWholeGraphService { get; set; }

        [Inject]
        private IJSRuntime _js { get; set; }

        [Inject]
        private ImportGraphState _importGraphState { get; set; }


        private async Task OnFileChangeAsync(InputFileChangeEventArgs e)
        {
            var file = e.File;
            if (file == null)
                return;

            _importGraphState.FileName = file.Name;

            using var memoryStream = new MemoryStream();
            await file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024).CopyToAsync(memoryStream);
            _importGraphState.File = new MemoryStream(memoryStream.ToArray());
        }

        private async Task Import()
        {
            if (_importGraphState.File is null)
                return;

            try
            {
                await _importGraphService.ExecuteAsync(_importGraphState.File);

                var graph = _getWholeGraphService.Execute();
                await _js.InvokeVoidAsync("graph.loadGraph", graph);
            }
            catch { }
        }
    }
}
