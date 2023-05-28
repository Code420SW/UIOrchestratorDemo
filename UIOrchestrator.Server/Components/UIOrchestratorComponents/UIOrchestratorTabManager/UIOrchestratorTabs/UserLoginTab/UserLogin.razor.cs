using System.Diagnostics.CodeAnalysis;
using Code420.UIOrchestrator.Core.Models.AuthP;
using Code420.UIOrchestrator.Core.Models.UserCredentials;
using Code420.UIOrchestrator.Server.Code.Models.Menus;
using Code420.UIOrchestrator.Server.Components.BaseComponents.ButtonBase;
using Code420.UIOrchestrator.Server.Components.BaseComponents.LabelBase;
using Code420.UIOrchestrator.Server.Components.BaseComponents.TextBoxBase;
using Code420.UIOrchestrator.Server.Components.CustomComponents.CustomToasts.GenericErrors;
using Code420.UIOrchestrator.Server.Components.CustomComponents.CustomToasts.Spinners;
using Code420.UIOrchestrator.Server.MediatR.User;
using MediatR;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Inputs;
using Code420.UIOrchestrator.Core.Models.UIOrchestratorConstants;
using Code420.UIOrchestrator.Server.Components.CompositeComponents.HelpButton;
using Code420.UIOrchestrator.Server.Components.CompositeComponents.HelpSystem.UserLogin.Email;

namespace Code420.UIOrchestrator.Server.Components.UIOrchestratorComponents.UIOrchestratorTabManager.UIOrchestratorTabs.UserLoginTab
{
    /// <summary>
    /// Provides the user login screen which is loaded as a Tab by the
    /// <see cref="UIOrchestratorTabManager"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This component <b>must</b> exist in the namespace defined by
    /// <see cref="UIOrchestratorConstants.OrchestratorTabBaseNamespace"/> otherwise
    /// an exception will be thrown when the application constructs the <see cref="RenderFragment"/>.
    /// </para>
    /// <para>
    /// This is the default Tab element loaded when the application starts.
    /// </para>
    /// <para>
    /// Other than the <see cref="OrchestratorRef"/>parameter, a reference to the
    /// <see cref="UIOrchestrator"/> passed though a <see cref="CascadingParameterAttribute"/>,
    /// the component has no parameters set by the consumer.
    /// </para>
    /// <para>
    /// The <see cref="MenuItemId"/> parameter is set by the application when the <see cref="RenderFragment"/>
    /// for the tab is constructed. This parameter <b>must</b> exist or an error will be generated.
    /// </para>
    /// <para>
    /// The <see cref="IMediator"/> object injected through DI.
    /// </para>
    /// <para>
    /// No public methods are exposed.
    /// </para>
    /// <para>
    /// The component consumes the TextBoxBase component to acquire the user's email address
    /// and password. A ButtonBase is consumed to initiate the login action. Two custom components
    /// <see cref="GenericError"/> and <see cref="GenericSpinner"/> are consumed to display messages
    /// to the user.
    /// </para>
    /// <para>
    /// The product of the component is a completed <see cref="UserLogoutCommandRequest"/>
    /// which is submitted to the IMediator <see cref="ISender.Send"/> method to authenticate
    /// the user through the API. The results of the API call is an updated <see cref="IUserCredentials"/>
    /// </para>
    /// <para>
    /// Upon a successful login, this component calls back to the <see cref="UIOrchestrator"/>
    /// passed though a <see cref="CascadingParameterAttribute"/> to inform the UIOrchestrator
    /// of the event.
    /// </para>
    /// </remarks>
    [SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]
    public partial class UserLogin : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        /// <summary>
        /// Contains the reference to the <see cref="UIOrchestrator"/> parent.
        /// <remarks>
        /// Note that this is a [CascadingParameter] passed down through the
        /// <see cref="UIOrchestratorTabManager"/>.
        /// </remarks>
        /// </summary>
        [CascadingParameter(Name = "OrchestratorRef")]
        public Pages.UIOrchestrator.UIOrchestrator OrchestratorRef { get; set; }

        /// <summary>
        /// String value containing the <see cref="OrchestratorMenuItem.ItemId"/> for this Tab.
        /// Used to identify the Tab when calling back to the <see cref="OrchestratorRef"/>.
        /// <remarks>
        /// As with all UIOrchestratorTabs, this parameter is set when the <see cref="RenderFragment"/>
        /// (encapsulated in the OrchestratorMenuItem.TabDefinition.ContentTemplate property) is built.
        /// </remarks>
        /// </summary>
        [Parameter]
        public string MenuItemId { get; set; }

        #endregion

        #endregion


        #region Instance Variables

        private const string emailPlaceholderPrompt = "Enter your email address...";
        private const string emailPlaceholderLabel = "Email Address:";
        private const string passwordPlaceholderPrompt = "Enter your password...";
        private const string passwordPlaceholderLabel = "Password:";
        private const string toastTarget = "#grid-main__user-login";
        private const FloatLabelType emailFloatLabelType = FloatLabelType.Auto;
        private const FloatLabelType passwordFloatLabelType = FloatLabelType.Auto;

        private TextBoxBase emailTextBoxBase;
        private TextBoxBase passwordTextBoxBase;
        private LabelBase labelPageHeader;
        private ButtonBase loginButton;
        private GenericError toastUserLoginError;
        private GenericSpinner toastUserLoginExecute;
        private HelpButton passwordHelpButton;
        private UserLoginEmailHelp emailHelpComponent;

        private readonly LoginUserModel loginUserModel = new();

        #endregion



        #region Injected Dependencies

        [Inject] private IMediator _mediator { get; set; }

        #endregion



        #region Private Methods for Internal Use Only

        /// <summary>
        /// Responsible for saving the contents of the email textbox to the loginUserModel.
        /// </summary>
        /// <param name="args">
        /// A <see cref="ChangedEventArgs"/> object containing the current value of the
        /// email text box.
        /// </param>
        private void OnEmailValueChange(ChangedEventArgs args)
        {
            //  All we need to do is invoke the Blur method since it handles saving the value
            //  This is done because with browser autofill the ValueChange event is fired but
            //  not the Blur event. Therefore, the prompts aren't updated correctly.
            //  TODO: Decide if ValueChange event can point directly to OnEmailBlurAsync()
            InvokeAsync(() => OnEmailBlurAsync(args.Value));
        }

        /// <summary>
        /// Responsible for saving the contents of the password textbox to the loginUserModel.
        /// </summary>
        /// <param name="args">
        /// A <see cref="ChangedEventArgs"/> object containing the current value of the
        /// password text box.
        /// </param>
        private void OnPasswordValueChange(ChangedEventArgs args)
        {
            //  All we need to do is invoke the Blur method since it handles saving the value
            //  This is done because with browser autofill the ValueChange event is fired but
            //  not the Blur event. Therefore, the prompts aren't updated correctly.
            //  TODO: Decide if ValueChange event can point directly to OnPasswordBlurAsync()
            InvokeAsync(() => OnPasswordBlurAsync(args.Value));
        }

        /// <summary>
        /// Invoked when the email textbox loses focus.
        /// Updates the text box's Placeholder property to either the emailPlaceholderPrompt or
        /// emailPlaceholderLabel value depending on if the textbox is empty.
        /// </summary>
        /// <param name="value">
        /// String value of the textbox contents.
        /// </param>
        /// <returns></returns>
        private async Task OnEmailBlurAsync(string value)
        {
            loginUserModel.Email = value;

            // Handle the case where the FloatLabelType is Auto...
            // If the contents is not empty then update the components Placeholder to the const
            // defined for the Placeholder Prompt which will be displayed inside the textbox.
            if (emailFloatLabelType == FloatLabelType.Auto)
            {
                var temp = loginUserModel is { Email: null or "" } 
                    ? emailPlaceholderPrompt 
                    : emailPlaceholderLabel;
                await emailTextBoxBase.UpdatePlaceholderAsync(temp);
            }
        }

        /// <summary>
        /// Invoked when the email textbox gains focus.
        /// Updates the text box's Placeholder property to the emailPlaceholderLabel value.
        /// </summary>
        /// <param name="value">
        /// String value of the textbox contents.
        /// </param>
        /// <returns></returns>
        // ReSharper disable once UnusedParameter.Local
        private async Task OnEmailFocusAsync(string value)
        {
            // Handle the case where the FloatLabelType is Auto...
            // Update the components Placeholder to the const defined for the Placeholder Label
            //  which will be displayed above the textbox.
            if (emailFloatLabelType == FloatLabelType.Auto) 
                await emailTextBoxBase.UpdatePlaceholderAsync(emailPlaceholderLabel);

        }

        /// <summary>
        /// Invoked when the password textbox loses focus.
        /// Updates the text box's Placeholder property to either the passwordPlaceholderPrompt or
        /// passwordPlaceholderLabel value depending on if the textbox is empty.
        /// </summary>
        /// <param name="value">
        /// String value of the textbox contents.
        /// </param>
        /// <returns></returns>
        private async Task OnPasswordBlurAsync(string value)
        {
            loginUserModel.Password = value;

            // Handle the case where the FloatLabelType is Auto...
            // If the contents is not empty then update the components Placeholder to the const defined for the Placeholder Prompt
            //  which will be displayed inside the textbox.
            if (passwordFloatLabelType == FloatLabelType.Auto)
            {
                var temp = loginUserModel.Password is null or "" 
                    ? passwordPlaceholderPrompt 
                    : passwordPlaceholderLabel;
                await passwordTextBoxBase.UpdatePlaceholderAsync(temp);
            }
        }

        /// <summary>
        /// Invoked when the password textbox gains focus.
        /// Updates the text box's Placeholder property to the passwordPlaceholderLabel value.
        /// </summary>
        /// <param name="value">
        /// String value of the textbox contents.
        /// </param>
        /// <returns></returns>
        // ReSharper disable once UnusedParameter.Local
        private async Task OnPasswordFocusAsync(string value)
        {
            // Handle the case where the FloatLabelType is Auto...
            // Update the components Placeholder to the const defined for the Placeholder Label
            //  which will be displayed above the textbox.
            if (passwordFloatLabelType == FloatLabelType.Auto) 
                await passwordTextBoxBase.UpdatePlaceholderAsync(passwordPlaceholderLabel);

        }

        /// <summary>
        /// Handles the Login button's click event.
        /// Performs a series of validations and reports any validation errors.
        /// Attempts to login the user and reports any errors.
        /// Invokes the <see cref="CleanupAndExit"/> method.
        /// </summary>
        private async void OnSubmit()
        {
            await loginButton.DisableAsync();

            //  Make sure a previous Toast is closed
            await toastUserLoginError.HideAsync();

            //  First a bit of property conditioning
            loginUserModel.Email = loginUserModel.Email?.Trim();
            loginUserModel.Password = loginUserModel.Password?.Trim();

            //  Customize and display the "Logging In" Toast
            toastUserLoginExecute.Title = $"Logging in {loginUserModel.Email}";
            await toastUserLoginExecute.ShowAsync();

            //  Invoke the login request and process any errors
            var status = await _mediator.Send(new UserLoginCommandRequest(loginUserModel));

            if (status.HasValidationErrors)
            {
                await DisplayValidationErrors(status.BuildValidationErrorMessage());
                await toastUserLoginExecute.HideAsync();
                await loginButton.EnableAsync();
                return;
            }

            if (status.HasErrors)
            {
                await DisplayExecutionErrors(status.BuildStatusErrorMessage());
                await toastUserLoginExecute.HideAsync();
                await loginButton.EnableAsync();
                return;
            }

            //  At this point, the user is successfully logged in.
            await toastUserLoginExecute.HideAsync();
            CleanupAndExit();
        }

        /// <summary>
        /// Configures and displays a Toast object to display the validation errors.
        /// </summary>
        /// <param name="errorMessage">
        /// String value containing the error message displayed in the Toast.
        /// </param>
        private async Task DisplayValidationErrors(string errorMessage)
        {
            //  Make sure a previous Toast is closed
            await toastUserLoginError.HideAsync();

            toastUserLoginError.Title = "User Credentials Error";
            toastUserLoginError.Content = errorMessage;
            await toastUserLoginError.ShowAsync();
        }

        /// <summary>
        /// Configures and displays a Toast object to display the login execution errors.
        /// </summary>
        /// <param name="errorMessage">
        /// String value containing the error message displayed in the Toast.
        /// </param>
        private async Task DisplayExecutionErrors(string errorMessage)
        {
            //  Make sure a previous Toast is closed
            await toastUserLoginError.HideAsync();

            //  Customize the "Login Error" Toast and display it
            toastUserLoginError.Title = "Login Error";
            toastUserLoginError.Content = errorMessage;
            await toastUserLoginError.ShowAsync();
        }

        /// <summary>
        /// Perform final cleanup and exit.
        /// </summary>
        private void CleanupAndExit()
        {
            //  Ensure all Toasts are closed.
            toastUserLoginError?.HideAsync();
            toastUserLoginExecute?.HideAsync();

            //  Callback to the UIOrchestrator to let it know the user is authorized
#pragma warning disable CS4014
            OrchestratorRef.UserLoginComplete();
#pragma warning restore CS4014
        }

        #endregion

    }
}
