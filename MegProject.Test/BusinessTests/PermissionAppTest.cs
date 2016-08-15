using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MegProject.Test.BusinessTests
{
    [TestClass]
    public class PermissionAppTest
    {
        private MegTestBase _testBase;
        [TestInitialize]
        public void Initialize()
        {
            _testBase = new MegTestBase();
        }

        [TestMethod]
        public void PermissionApp_Base_İzin_Ekleme_Islemini_Yapar()
        {
           var test = _testBase.CreatePermission();
           Assert.IsNotNull(test,"Permission oluşturulamadı!");
           
        }
        [TestMethod]
        public void Permission_Izin_Guncelleme_Islemini_Yapar()
        {
            var test = _testBase.UpdatePermissions();
            Assert.IsNotNull(test,"Güncelleme işlemi başarısız!");
        }
    }
}
