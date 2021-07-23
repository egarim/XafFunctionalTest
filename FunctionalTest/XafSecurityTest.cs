using DevExpress.Data.Linq.Helpers;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

using NUnit.Framework;
using System;
using XafFunctionalTest.Module;
using XafFunctionalTest.Module.BusinessObjects;
using XafFunctionalTest.Module.DatabaseUpdate;

namespace FunctionalTest
{
    public class XafSecurityTest
    {

        private Customer Customer;
        private FakeAppearanceTarget target;
        private AppearanceController controller;
        private DetailView detailView;
        SecurityStrategyComplex security;
        SecuredObjectSpaceProvider secureobjectSpaceProvider;
        TestApplication application;
        XPObjectSpaceProvider nonSecureObjectSpaceProvider;
        MemoryDataStoreProvider dataStoreProvider;




        [SetUp]
        public virtual void SetUp()
        {

            //HACK   https://docs.devexpress.com/eXpressAppFramework/112769/getting-started/in-depth-tutorial-winforms-webforms/security-system/access-the-security-system-in-code

            dataStoreProvider = new MemoryDataStoreProvider();


            //Create an instance of the test application (this application is a core application and is not bound to any U.I platform)
            application = new TestApplication();
            nonSecureObjectSpaceProvider = new XPObjectSpaceProvider(dataStoreProvider);
            XafFunctionalTestModule xafFunctionalTestModule = new XafFunctionalTestModule();
            application.Modules.Add(xafFunctionalTestModule);

            application.Setup("TestApplication", nonSecureObjectSpaceProvider);


            //Create an object space provider that is not secure
            var UpdaterObjectSpace = nonSecureObjectSpaceProvider.CreateUpdatingObjectSpace(true);



            //Create a instance of the updater from the agnostic module
            Updater updater = new Updater(UpdaterObjectSpace, null);
            updater.UpdateDatabaseBeforeUpdateSchema();
            updater.UpdateDatabaseAfterUpdateSchema();




        }

        private SecurityStrategyComplex Login(string userName, string password)
        {
            var LoginObjectSpace = nonSecureObjectSpaceProvider.CreateObjectSpace();
            AuthenticationStandard authentication = new AuthenticationStandard();
            this.security = new SecurityStrategyComplex(typeof(ApplicationUser), typeof(PermissionPolicyRole), authentication);
            security.RegisterXPOAdapterProviders();

            authentication.SetLogonParameters(new AuthenticationStandardLogonParameters(userName, password));
            security.Logon(LoginObjectSpace);
            secureobjectSpaceProvider = new SecuredObjectSpaceProvider(this.security, dataStoreProvider);
            application.Security = security;
            return security;
        }

        [Test]
        public void TestRoles()
        {

            SecurityStrategyComplex security = Login("User", "");

            IObjectSpace objectSpace = secureobjectSpaceProvider.CreateObjectSpace();
            SecurityStrategy SecurityFromApp = application.GetSecurityStrategy();

          

            Assert.Throws<UserFriendlyObjectLayerSecurityException>
            (
              () =>
              {
                  Customer = objectSpace.CreateObject<Customer>();
                  objectSpace.CommitChanges();

              }

            );

           
         
            //target = new FakeAppearanceTarget();
            //controller = new AppearanceController();
            //detailView = application.CreateDetailView(objectSpace, Customer);
            //controller.SetView(detailView);
        }
        [Test]
        public void TestRoles2()
        {

            SecurityStrategyComplex security = Login("Admin", "");

            IObjectSpace objectSpace = secureobjectSpaceProvider.CreateObjectSpace();
            SecurityStrategy SecurityFromApp = application.GetSecurityStrategy();



            Customer = objectSpace.CreateObject<Customer>();
            objectSpace.CommitChanges();
            Assert.Pass();
        }

    }
}