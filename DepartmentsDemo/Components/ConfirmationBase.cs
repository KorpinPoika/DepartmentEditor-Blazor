using Microsoft.AspNetCore.Components;

namespace DepartmentsDemo.Components;

public class ConfirmationBase: ComponentBase
{
	protected bool ShowConfirmation { get; private set; }

	[Parameter]
	public string ConfirmationTitle { get; set; } = "Confirmation";

	[Parameter]
	public string ConfirmationMessage { get; set; } = "Are you sure?";

	[Parameter]
	public string OkButtonTitle { get; set; } = "Yes";

	[Parameter]
	public string CancelButtonTitle { get; set; } = "No";

	public void Show()
	{
		ShowConfirmation = true;
		StateHasChanged();
	}

	[Parameter]
	public EventCallback<bool> ConfirmationChanged { get; set; }

	protected async Task OnConfirmationChangeAsync(bool value)
	{
		ShowConfirmation = false;
		await ConfirmationChanged.InvokeAsync(value);
	}	
}