using Microsoft.JSInterop;

namespace Sneat.Pro.Client.Layout;

public partial class MainLayout
{
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await jsRuntime.InvokeVoidAsync("framework.init");
        }
        base.OnAfterRender(firstRender);
    }
}