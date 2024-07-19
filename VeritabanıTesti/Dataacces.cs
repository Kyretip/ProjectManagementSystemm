using DataAccess.Abstracts;
using DataAccess.Concretes.EntityFramework;
using Entities.Concretes;
using System.Diagnostics;
using System;
using System.Text;
namespace VeritabanıTesti
{
    public class Tests
    {
        private EfTaskListRepository TaskListRepository;
        private TaskList TaskList;
        Random random = new Random();
        List<TaskList> LargeList = new List<TaskList>();

        [SetUp]
        public void Setup()
        {
            TaskListRepository = new EfTaskListRepository();
            TaskList = new TaskList();
            TaskList.Id = 0;
            TaskList.BoardId = 2;
            TaskList.OrderNo = 5;
            TaskList.Name = "TeeskLiesstt";
            for (int x = 0; x <= 500; x++)
            {
                LargeList.Add(new TaskList { Id = 0, BoardId = random.Next(0, 100), OrderNo =x, Name = GenerateRandomString(10) });
            }
        }

        [Test]
       
        public void taskEkle()
        {
            Assert.AreEqual(TaskList.Name, "TeeskLiesstt"); // Eklenecek TaskList'in adının doğrulanması 
            TaskListRepository.Add(TaskList); // TaskList eklenmesi
            var retrievedTaskList = TaskListRepository.Get(p => p.Id.Equals(TaskList.Id)); // Tasklist'in eklenip eklenmediğinin kontrolü 
            Assert.IsNotNull(retrievedTaskList); // Null kontrolü */
            Assert.AreEqual("TeeskLiesstt", retrievedTaskList.Name); // Sonuç Kontrolü yapıyoruz 
        }

        [Test]
        /* ID'si bilinen task listesinin veritabanından çekilmesi */
        public void taskGetir()
        {
            var sonuc = TaskListRepository.Get(p => p.Id.Equals(1));
            Assert.AreEqual(sonuc.Name, TaskList.Name);
        }

        [Test]
        /* Task listesindeki bütün satırların çekilmesi */
        public void butunTasklariGetir()
        {
            var butunListe = TaskListRepository.GetAll();
            Assert.IsNotNull(butunListe); // liste null kontrolü 
            foreach (var item in butunListe)
            {
                // Her bir elemanın tip kontrolünün yapılması 
                Assert.IsInstanceOf<int>(item.Id, $"TaskList ID'si int türünde değil: {item.Id}");
                Assert.IsInstanceOf<String>(item.Name, $"TaskList adı String türünde değil: {item.Name}");
                Assert.IsInstanceOf<int>(item.OrderNo, $"TaskList sıra numarası int türünde değil: {item.OrderNo}");
                Assert.IsInstanceOf<int>(item.BoardId, $"TaskList board ID'si int türünde değil: {item.BoardId}");
            }
        }

        [Test]
        /* Task listesine liste şeklinde yükleme */
        public void topluEkleme()
        {
            foreach (var item in LargeList)
            {
                TaskListRepository.Add(item); // Listedeki elemanların hepsinin yüklenmesi               
            }

            foreach (var item in LargeList)
            {
                var sonuc = TaskListRepository.Get(p => p.Name.Equals(item.Name));
                Assert.AreEqual(sonuc.OrderNo, item.OrderNo);
            }
        }

        /* Test için string üreten metod */
        private static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }

            return stringBuilder.ToString();
        }

    }

}

