using Code420.CanXtracServer.Pages.UIOrchestrator;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using Code420.CanXtracCore.Models.AuthP;
using Code420.CanXtracServer.Components.BaseComponents.ButtonBase;
using Code420.CanXtracServer.Components.BaseComponents.LabelBase;
using Code420.CanXtracServer.Components.BaseComponents.TextBoxBase;
using Code420.CanXtracServer.Components.CustomComponents.CustomToasts.Spinners;
using Syncfusion.Blazor.Inputs;
using MediatR;
using Code420.CanXtracServer.Code.Models.Menus;
using Code420.CanXtracServer.Components.CustomComponents.CustomToasts.GenericErrors;

namespace Code420.CanXtracServer.Components.UIOrchestratorComponents.UIOrchestratorTabManager.UIOrchestratorTabs.ConfigurationTab.ConfigurationChildTabs.CompanyInfoTab
{
    /// <summary>
    /// Responsible for managing all Orchestrator Tab Items.
    /// </summary>
    /// <remarks>
    /// <para>
    /// 1.  X
    /// </para>
    /// <para>
    /// 2.  X
    /// </para>
    /// <para>
    /// 3.  X
    /// </para>
    /// <para>
    /// 4.  X
    /// </para>
    /// <para>
    /// 5.  X
    /// </para>
    /// <para>
    /// 6.  X
    /// </para>
    /// </remarks>
    public partial class CompanyInfo : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        // ==================================================
        // Base Parameters
        // ==================================================

        /// <summary>
        /// Contains the reference to the <see cref="UIOrchestrator"/> parent.
        /// </summary>
        [CascadingParameter(Name = "OrchestratorRef")]
        public UIOrchestrator OrchestratorRef { get; set; }


        /// <summary>
        /// String value containing the <see cref="OrchestratorMenuItem.ItemId"/> for this Tab.
        /// Used to identify the Tab when calling back to the <see cref="OrchestratorRef"/>.
        /// </summary>
        [Parameter]
        public string MenuItemId { get; set; }

        #endregion


        #region Event Callback Parameters

        // ==================================================
        // Event Callback Parameters
        // ==================================================


        #endregion


        #region CSS Parameters

        // ==================================================
        // CSS Styling Parameters
        // ==================================================


        #endregion

        #endregion



        #region Callback Events Invoked from Underlying Components

        // ==================================================
        // Methods used as Callback Events from the underlying component(s)
        // ==================================================

        //private void MyAddedHandler(AddEventArgs args)
        //{
        //    //Debug.WriteLine("MyAddedHandler method invoked.");
        //}

        //private void MyAddingHandler(AddEventArgs args)
        //{
        //    //Debug.WriteLine("MyAddingHandler method invoked.");
        //}

        //private void MyCreatedHandler(object args)
        //{
        //    //Debug.WriteLine("MyCreatedHandler method invoked.");
        //}

        //private void MyDestroyedHandler(object args)
        //{
        //    //Debug.WriteLine("MyDestroyedHandler method invoked.");
        //}

        //private void MyDraggedHandler(DragEventArgs args)
        //{
        //}

        //private void MyOnDragStartHandler(DragEventArgs args)
        //{
        //}

        //private void MyOnRemovedHandler(RemoveEventArgs args)
        //{
        //}

        //private void MyOnRemovingHandler(RemoveEventArgs args)
        //{
        //}

        //private void MyOnSelectedHandler(SelectEventArgs args)
        //{
        //}

        //private void MyOnSelectingHandler(SelectingEventArgs args)
        //{
        //}

        // private void MyOnSelectedItemChangedHandler(int index)
        // {
        //     // Update the active tab index tracker
        //     /////selectedItem = index;
        // }



        #endregion



        #region Instance Variables

        // ==================================================
        // Instance variables
        // ==================================================

        private const string emailPlaceholderPrompt = "Enter your email address...";    // Placeholder displayed when the email textbox is inactive and empty
        private const string emailPlaceholderLabel = "Email Address:";                  // Label displayed above the email textbox when it is active, or inactive and not empty
        private const string passwordPlaceholderPrompt = "Enter your password...";      // Placeholder displayed when the password textbox is inactive and empty
        private const string passwordPlaceholderLabel = "Password:";                    // Label displayed above the password textbox when it is active, or inactive and not empty
        private const string toastTarget = "#grid-main__user-login";                    // CSS ID that a toast will display in.
        private const FloatLabelType emailFloatLabelType = FloatLabelType.Auto;         // FloatLabelType setting for the email textbox
        private const FloatLabelType passwordFloatLabelType = FloatLabelType.Auto;      // FloatLabelType setting for the password textbox

        private TextBoxBase emailTextBoxBase;
        private TextBoxBase passwordTextBoxBase;
        private LabelBase labelPageHeader;
        private ButtonBase loginButton;

        private GenericError toastUserLoginError;
        private GenericSpinner toastUserLoginExecute;

        private readonly LoginUserModel loginUserModel = new();                      // The base model for this tab

        #endregion



        #region Injected Dependencies

        // Injected Dependencies


        // Dependency Injection
        [Inject] private IMediator _mediator { get; set; }

        #endregion



        #region Constructors


        // ==================================================
        // Constructors
        // ==================================================

        // This method is executed whenever the parent renders.
        // Parameters that were passed into the component are contained in a ParameterView.
        //  This is a good point at which to make asynchronous calls to a server (for example)
        //  based on the state passed into the component.
        // The component’s [Parameter] properties are assigned their values when you call
        //  base.SetParametersAsync(parameters) inside your override.
        // It is also the correct place to assign default parameter values.
        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);
        }

        // Once the state from the ParameterCollection has been assigned to the component’s
        //  [Parameter] properties, these methods are executed. This is useful in the same way
        //  as SetParametersAsync, except it is possible to use the component’s state.
        // This method is only executed once when the component is first created.If the parent
        //  changes the component’s parameters at a later time, this method is skipped.
        // When the component is a @page, and our Blazor app navigates to a new URL that renders
        //  the same page, Blazor will reuse the current object instance for that page.Because the
        //  object is the same instance, Blazor does not call IDisposable.Dispose on the object,
        //  nor does it execute its OnInitialized method again.
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        // This method will be executed immediately after OnInitializedAsync if this is a new
        //  instance of a component. If it is an existing component that is being re-rendered because
        //  its parent is re-rendering then the OnInitialized* methods will not be executed, and this
        //  method will be executed immediately after SetParametersAsync instead
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
        }

        // This is the first place that the State should be changed
        //
        // This method is executed every time Blazor has re-generated the component’s RenderTree.
        //  This can be as a result of the component’s parent re-rendering, the user interacting with the component
        //  (e.g. a mouse-click), or if the component executes its StateHasChanged method to invoke a re-render.
        // This method has a single parameter named firstRender. This parameter is true only the first time the
        //  method is called on the current component, from there onwards it will always be false. In cases where
        //  additional component hook-up is required (for example, via JavaScript) it is useful to know this is the
        //  first render.
        // It is not until after the OnAfterRender method have executed that it is safe to use any references to
        //  components set via the @ref directive. And it is not until after the OnAfterRender method have been
        //  executed with firstRender set to true that it is safe to use any references to HTML elements set via
        //  the @ref directive
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
        }

        #endregion



        #region Public Methods Providing Access to the Underlying Components to the Consumer

        // ==================================================
        // Public Methods providing access to the underlying component to the consumer
        // ==================================================



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
        /// Updates the text box's Placeholder property to either the emailPlaceholderPrompt or emailPlaceholderLabel value
        /// depending on if the textbox is empty.
        /// </summary>
        /// <param name="value">String value of the textbox contents.</param>
        /// <returns></returns>
        private async Task OnEmailBlurAsync(string value)
        {
            this.loginUserModel.Email = value;

            // Handle the case where the FloatLabelType is Auto...
            // If the contents is not empty then update the components Placeholder to the const defined for the Placeholder Prompt
            //  which will be displayed inside the textbox.
            if (emailFloatLabelType == FloatLabelType.Auto)
            {
                string temp = (this.loginUserModel.Email is null || this.loginUserModel.Email == "") ? emailPlaceholderPrompt : emailPlaceholderLabel;
                await this.emailTextBoxBase.UpdatePlaceholderAsync(temp);
            }
        }

        /// <summary>
        /// Invoked when the email textbox gains focus.
        /// Updates the text box's Placeholder property to the emailPlaceholderLabel value.
        /// </summary>
        /// <param name="value">String value of the textbox contents.</param>
        /// <returns></returns>
        // ReSharper disable once UnusedParameter.Local
        private async Task OnEmailFocusAsync(string value)
        {
            // Handle the case where the FloatLabelType is Auto...
            // Update the components Placeholder to the const defined for the Placeholder Label
            //  which will be displayed above the textbox.
            if (emailFloatLabelType == FloatLabelType.Auto)
            {
                await emailTextBoxBase.UpdatePlaceholderAsync(emailPlaceholderLabel);
            }

        }


        /// <summary>
        /// Invoked when the password textbox loses focus.
        /// Updates the text box's Placeholder property to either the passwordPlaceholderPrompt or passwordPlaceholderLabel value
        /// depending on if the textbox is empty.
        /// </summary>
        /// <param name="value">String value of the textbox contents.</param>
        /// <returns></returns>
        private async Task OnPasswordBlurAsync(string value)
        {
            this.loginUserModel.Password = value;

            // Handle the case where the FloatLabelType is Auto...
            // If the contents is not empty then update the components Placeholder to the const defined for the Placeholder Prompt
            //  which will be displayed inside the textbox.
            if (passwordFloatLabelType == FloatLabelType.Auto)
            {
                string temp = (this.loginUserModel.Password is null || this.loginUserModel.Password == "") ? passwordPlaceholderPrompt : passwordPlaceholderLabel;
                await this.passwordTextBoxBase.UpdatePlaceholderAsync(temp);
            }
        }


        /// <summary>
        /// Invoked when the password textbox gains focus.
        /// Updates the text box's Placeholder property to the passwordPlaceholderLabel value.
        /// </summary>
        /// <param name="value">String value of the textbox contents.</param>
        /// <returns></returns>
        // ReSharper disable once UnusedParameter.Local
        private async Task OnPasswordFocusAsync(string value)
        {
            // Handle the case where the FloatLabelType is Auto...
            // Update the components Placeholder to the const defined for the Placeholder Label
            //  which will be displayed above the textbox.
            if (passwordFloatLabelType == FloatLabelType.Auto)
            {
                await passwordTextBoxBase.UpdatePlaceholderAsync(passwordPlaceholderLabel);
            }

        }


        /// <summary>
        /// Handles the Login button's click event.
        /// Performs a series of validations and reports any validation errors.
        /// Attempts to login the user and reports any errors.
        /// Invokes the <see cref="CleanupAndExit"/> method.
        /// </summary>
        private async void OnSubmit()
        {
            //  Disable the Login button to prevent multiple execution
            await this.loginButton.DisableAsync();

            //  Make sure a previous Toast is closed
            await this.toastUserLoginError.HideAsync();

            //  First a bit of field conditioning
            this.loginUserModel.Email = this.loginUserModel.Email?.Trim();
            this.loginUserModel.Password = this.loginUserModel.Password?.Trim();

            //  Customize and display the "Logging In" Toast
            this.toastUserLoginExecute.Title = $"Logging in {loginUserModel.Email}";
            await this.toastUserLoginExecute.ShowAsync();

            //  Invoke MediatR pipeline for the LoginUserModel
            var status = await _mediator.Send(loginUserModel);

            //  Handle validation errors
            if (status.HasValidationErrors)
            {
                //  Display validation errors, enable the Login button and bail
                DisplayValidationErrors(status.BuildValidationErrorMessage());
                await this.toastUserLoginExecute.HideAsync();
                await this.loginButton.EnableAsync();
                return;
            }

            //  Handle execution errors
            if (status.HasErrors)
            {
                //  Display execution errors, enable the Login button and bail
                DisplayExecutionErrors(status.BuildStatusErrorMessage());
                await this.toastUserLoginExecute.HideAsync();
                await this.loginButton.EnableAsync();
                return;
            }

            //  Hide the "Logging In" Toast
            await this.toastUserLoginExecute.HideAsync();

            //  At this point, the user is successfully logged in.
            CleanupAndExit();
        }


        /// <summary>
        /// Configures and displays a Toast object to display the validation errors.
        /// </summary>
        /// <param name="errorMessage">String value containing the error message displayed in the Toast.</param>
        private async void DisplayValidationErrors(string errorMessage)
        {
            //  Make sure a previous Toast is closed
            await this.toastUserLoginError.HideAsync();

            this.toastUserLoginError.Title = "User Credentials Error";
            this.toastUserLoginError.Content = errorMessage;

            await this.toastUserLoginError.ShowAsync();
        }


        /// <summary>
        /// Configures and displays a Toast object to display the login execution errors.
        /// </summary>
        /// <param name="errorMessage">String value containing the error message displayed in the Toast.</param>
        private async void DisplayExecutionErrors(string errorMessage)
        {
            //  Make sure a previous Toast is closed
            await this.toastUserLoginError.HideAsync();

            //  Customize the "Login Error" Toast and display it
            this.toastUserLoginError.Title = "Login Error";
            this.toastUserLoginError.Content = errorMessage;
            await this.toastUserLoginError.ShowAsync();
        }


        /// <summary>
        /// Perform final cleanup and exit.
        /// </summary>
        private void CleanupAndExit()
        {
            //  Ensure all Toasts are closed.
            toastUserLoginError?.HideAsync();
            toastUserLoginExecute?.HideAsync();

            //  Call-back to the UIOrchestrator to let it know the user is authorized
#pragma warning disable CS4014
            OrchestratorRef.UserLoginComplete();
#pragma warning restore CS4014
        }

        #endregion

    }
}
