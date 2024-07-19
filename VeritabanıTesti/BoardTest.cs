using DataAccess.Concretes.EntityFramework;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VeritabanıTesti
{
    public class BoardTest
    {
        private EfBoardRepository boarddb;
        Board boardToBeDeleted;
        private string name;
        string newname;
       [SetUp]
        public void Setup()
        {
            name = "BOARDNAME";
            newname = "BOARDNEWNAME";
            boarddb = new EfBoardRepository();
            boardToBeDeleted = new Board
            {
                Id = 0, 
                WorkspaceId = 1, 
                CreatedUserId = 1, 
                Name = name, 
                PrivateToWorkspaceMember = false, 
                CreatedDate = DateTime.Now, 
                EndDate = DateTime.Now.AddMonths(6) 
            };
        }

        [Test]
        public void BoardEkle()
        {
            string namee = name;
            boarddb.Add(boardToBeDeleted);
            var returnboard = boarddb.Get(p => p.Name.Equals(name));
            
            Assert.AreEqual(name, boardToBeDeleted.Name);
        }

        [Test]
        public void Boardupdate()
        {
            var returnboard = boarddb.Get(p => p.Name.Equals(name));
            returnboard.Name = newname;
            boarddb.Update(returnboard);
            returnboard = boarddb.Get(p => p.Name.Equals(newname));
            Assert.AreEqual(newname, returnboard.Name);


            
        }

        [Test]
        public void BoarSil()
        {
            var returnboard = boarddb.Get(p => p.Name.Equals(newname));
            boarddb.Delete(returnboard);
            returnboard = boarddb.Get(p => p.Name.Equals(newname));
            Assert.AreEqual(returnboard, null);

        }

    }
}
