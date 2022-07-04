using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.IO;

namespace AbrvDicaryTest
{
    [TestClass]
    public class MariaUtilTest
    {
            
        [TestMethod]
        public void TestOpen()
        {
            bool result = AbrvDicary.MariaUtil.Instance.Open();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestClose()
        {
            bool result = AbrvDicary.MariaUtil.Instance.Close();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestSelect()
        {
            string query = "";
            query += "SELECT word_id";
            query += "       ,word_nm";
            query += "       ,word_en_nm";
            query += "       ,word_abrv_nm";
            query += "       ,word_dc";
            query += "  FROM word";

            DataTable words = AbrvDicary.MariaUtil.Instance.Select(query);
            bool result = (words.Rows.Count > 0) ? true : false;
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestSelectWordsWithWordNmKeyword()
        {
            string keyword = "»çÀü";
            string query = "";
            query += "SELECT word_id";
            query += "       ,word_nm";
            query += "       ,word_en_nm";
            query += "       ,word_abrv_nm";
            query += "       ,word_dc";
            query += "  FROM word";
            query += " WHERE MATCH(word_nm) AGAINST('" + keyword + "' IN BOOLEAN MODE)";

            DataTable words = AbrvDicary.MariaUtil.Instance.Select(query);
            bool result = (words.Rows.Count > 0) ? true : false;
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestSelectWordsWithWordEnNmKeyword()
        {
            string keyword = "dictionary";
            string query = "";
            query += "SELECT word_id";
            query += "       ,word_nm";
            query += "       ,word_en_nm";
            query += "       ,word_abrv_nm";
            query += "       ,word_dc";
            query += "  FROM word";
            query += " WHERE MATCH(word_en_nm) AGAINST('" + keyword + "' IN BOOLEAN MODE)";

            DataTable words = AbrvDicary.MariaUtil.Instance.Select(query);
            bool result = (words.Rows.Count > 0) ? true : false;
            Assert.IsTrue(result);
        }

    }
}
