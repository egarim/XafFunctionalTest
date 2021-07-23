using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XafFunctionalTest.Module.BusinessObjects;

namespace XafFunctionalTest.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CustomerController : ViewController
    {
        public const string CustomerActionId = "Customer Action";
        SimpleAction CustomerAction;
        public CustomerController()
        {
            InitializeComponent();
            this.TargetObjectType = typeof(Customer);
            this.TargetViewType = ViewType.DetailView;

            CustomerAction = new SimpleAction(this, CustomerActionId, "View");
            CustomerAction.Execute += CustomerAction_Execute;
            CustomerAction.TargetObjectsCriteria = "Active = true";
            CustomerAction.TargetObjectType = typeof(Customer);
            CustomerAction.TargetObjectsCriteriaMode = TargetObjectsCriteriaMode.TrueAtLeastForOne;
            CustomerAction.TargetViewType = ViewType.DetailView;
            CustomerAction.TargetViewNesting = Nesting.Any;
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private void CustomerAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            // Execute your business logic (https://docs.devexpress.com/eXpressAppFramework/112737/).
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
