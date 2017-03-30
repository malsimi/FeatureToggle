﻿using System.Threading.Tasks;
using Xunit;

#if NETFX_CORE
using FeatureToggle.UAP.Tests.TestHelpers;
#endif

// ReSharper disable CheckNamespace
namespace FeatureToggle.Shared.Tests.Integration
// ReSharper restore CheckNamespace
{
    [Trait("category", "Threaded")]
    public class EnabledOnOrAfterToggleAssemblyVersionShould
    {
        // Test assembly version should be set to 0.0.2.1

        [Fact]
        public async Task BeEnabledOnExactMatchingVersion()
        {
            var result = false;
            var sut = new MyVersionToggleFor_v0_0_2_1();

#if NETFX_CORE
            await RunOn.Dispatcher(() =>
            {
                ApplicationResourcesSetter.Set("FeatureToggle.MyVersionToggleFor_v0_0_2_1", "0.0.2.1");
                result = sut.FeatureEnabled;
            });
#else
            result = sut.FeatureEnabled;
#endif

            Assert.True(result);
        }



        [Fact]
        public async Task BeDisabledWhenAssemblyVersionIsBelowConfiguredAssemblyVersion()
        {
            var result = true;
            var sut = new MyVersionToggleFor_v0_0_2_2();


#if NETFX_CORE
            await RunOn.Dispatcher(() =>
            {
                ApplicationResourcesSetter.Set("FeatureToggle.MyVersionToggleFor_v0_0_2_2", "0.0.2.2");
                result = sut.FeatureEnabled;
            });
#else
            result = sut.FeatureEnabled;
#endif

            Assert.False(result);
        }


        [Fact]
        public async Task BeEnabledWhenAssemblyVersionIsAboveConfiguredAssemblyVersion()
        {
            var result = false;
            var sut = new MyVersionToggleFor_v0_0_2_0();

#if NETFX_CORE
            await RunOn.Dispatcher(() =>
            {
                ApplicationResourcesSetter.Set("FeatureToggle.MyVersionToggleFor_v0_0_2_0", "0.0.2.0");
                result = sut.FeatureEnabled;
            });
#else
            result = sut.FeatureEnabled;
#endif
            Assert.True(result);
        }

        // ReSharper disable InconsistentNaming
        private class MyVersionToggleFor_v0_0_2_0 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
        private class MyVersionToggleFor_v0_0_2_1 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
        private class MyVersionToggleFor_v0_0_2_2 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
        // ReSharper restore InconsistentNaming
    }
}



//using System.Threading.Tasks;
//using FeatureToggle;
//using Xunit;

//#if NETFX_CORE
//using FeatureToggle.UAP.Tests.TestHelpers;
//#endif

//namespace FeatureToggle.Shared.Tests.Integration
//{
//    public class EnabledOnOrAfterToggleAssemblyVersionShould
//    {

//        // Test assembly requires specific assembly version for these tests

//        [Fact]
//        public void BeEnabledOnExactMatchingVersion()
//        {
//            var sut = new MyVersionToggleFor_v0_0_2_1();

//            Assert.True(sut.FeatureEnabled);
//        }


//        [Fact]
//        public async Task BeDisabledWhenAssemblyVersionIsBelowConfiguredAssemblyVersion()
//        {
//            var sut = new MyVersionToggleFor_v0_0_2_2();

//            bool result = true;
//#if NETFX_CORE            
//            await RunOn.Dispatcher(() =>
//            {
//                ApplicationResourcesSetter.Set("FeatureToggle.MyVersionToggleFor_v0_0_2_2", "0.0.2.1");
//                result = sut.FeatureEnabled; 
//            });
//#else
//            result = sut.FeatureEnabled;
//#endif
//            Assert.False(result);
//        }

//        [Fact]
//        public void BeEnabledWhenAssemblyVersionIsAboveConfiguredAssemblyVersion()
//        {
//            var sut = new MyVersionToggleFor_v0_0_2_0();

//            Assert.True(sut.FeatureEnabled);
//        }

//// ReSharper disable InconsistentNaming
//        private class MyVersionToggleFor_v0_0_2_0 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
//        private class MyVersionToggleFor_v0_0_2_1 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
//        private class MyVersionToggleFor_v0_0_2_2 : EnabledOnOrAfterAssemblyVersionWhereToggleIsDefinedToggle { }
//// ReSharper restore InconsistentNaming
//    }
//}