using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using Exam_MusicalShop.Models;

namespace Exam_MusicalShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>



    public partial class MainWindow : Window
    {
        MusicalContext db;
        List<string> filterList;
        public MainWindow()
        {
            db = new MusicalContext();
            filterList = new List<string>();
            InitializeComponent();

            LoadDisks();
            LoadComboBoxes();
        }

        class DiskView
        {
            public int DiskId { get; set; }
            public string Name { get; set; }

            public string BandName { get; set; }
            public int BandId { get; set; }

            public string GenreName { get; set; }
            public int GenreId { get; set; }

            public string PublisherName { get; set; }
            public int PublisherId { get; set; }

            public int PublishYear { get; set; }
            public decimal DiskSelfPrice { get; set; }
            public decimal Money { get; set; }
            public int Amount { get; set; }
        }


        public void LoadDisks()
        {
            musicListView.Items.Clear();
            List<Disk> disks = db.Disks.ToList();
            DiskView d = null;
            double percent = 0;
            decimal money = 0;
            foreach (Disk disk in disks)
            {
                d = new DiskView();

                d.DiskId = disk.Id;
                d.Name = disk.Name;
                d.BandName = db.Bands.FirstOrDefault(p => p.Id == disk.BandId).Name;
                d.BandId = disk.BandId;

                d.GenreName = db.Genres.FirstOrDefault(p => p.Id == disk.GenreId).Name;
                d.GenreId = disk.GenreId;

                d.PublisherName = db.Publishers.FirstOrDefault(p => p.Id == disk.PublisherId).Name;
                d.PublisherId = disk.PublisherId;

                d.PublishYear = disk.PublishYear;

                percent = db.Prices.FirstOrDefault(p => p.DiskId == disk.Id).Percent / 100.0;
                money = disk.SelfPrice + disk.SelfPrice * Convert.ToDecimal(percent);
                d.Money = money;

                d.DiskSelfPrice = disk.SelfPrice;
                d.Amount = db.Storages.FirstOrDefault(s => s.DiskId == disk.Id).Amount;

                musicListView.Items.Add(d);
            }
        }

        public void LoadComboBoxes()
        {
            List<Band> bands = db.Bands.ToList();
            foreach (Band band in bands)
            {
                ComboDiskBand.Items.Add(band);
                ComboDiskBand.DisplayMemberPath = "Name";
            }

            List<Genre> genres = db.Genres.ToList();
            foreach (Genre genre in genres)
            {
                ComboDiskGenre.Items.Add(genre);
                ComboDiskGenre.DisplayMemberPath = "Name";
            }

            List<Publisher> publishers = db.Publishers.ToList();
            foreach (Publisher publisher in publishers)
            {
                ComboDiskPublisher.Items.Add(publisher);
                ComboDiskPublisher.DisplayMemberPath = "Name";
            }

            filterList.Add("По новизні");
            filterList.Add("По популярності диску");
            filterList.Add("По популярності автора");
            filterList.Add("По популярності жанру");

            foreach (string name in filterList)
            {
                ComboDiskFilter.Items.Add(name);
            }

        }

        private void MusicListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (musicListView.SelectedItem != null)
            {
                DiskView diskView = musicListView.SelectedItem as DiskView;

                BoxDiscName.Text = diskView.Name;
                ComboDiskBand.SelectedItem = db.Bands.FirstOrDefault(b => b.Id == diskView.BandId);
                ComboDiskGenre.SelectedItem = db.Genres.FirstOrDefault(g => g.Id == diskView.GenreId);
                ComboDiskPublisher.SelectedItem = db.Publishers.FirstOrDefault(p => p.Id == diskView.PublisherId);
                BoxDiskYear.Text = diskView.PublishYear.ToString();
                BoxDiskMoney.Text = diskView.DiskSelfPrice.ToString();//db.Disks.FirstOrDefault(d => d.Id == diskView.DiskId).SelfPrice.ToString();

                BoxPricePercent.Text = db.Prices.FirstOrDefault(p => p.DiskId == diskView.DiskId).Percent.ToString();
                BoxStorageAmount.Text = db.Storages.FirstOrDefault(s => s.DiskId == diskView.DiskId).Amount.ToString();
            }
        }

        private void BtnDiskDelete_Click(object sender, RoutedEventArgs e)
        {
            DiskView diskView = new DiskView();
            diskView = musicListView.SelectedItem as DiskView;

            Storage storage = db.Storages.FirstOrDefault(s => s.DiskId == diskView.DiskId);
            db.Storages.Remove(storage);
            Price price = db.Prices.FirstOrDefault(p => p.DiskId == diskView.DiskId);
            db.Prices.Remove(price);
            if (db.DiscountDisks.Where(d => d.DiskId == diskView.DiskId).Count() > 0)
            {
                DiscountDisk discount = db.DiscountDisks.FirstOrDefault(d => d.DiskId == diskView.DiskId);
                db.DiscountDisks.Remove(discount);
            }

            Disk disk = db.Disks.FirstOrDefault(d => d.Id == diskView.DiskId);
            db.Disks.Remove(disk);
            db.SaveChanges();
            LoadDisks();
        }

        private void BtnDiskAdd_Click(object sender, RoutedEventArgs e)
        {

            int bandId = db.Bands.FirstOrDefault(b => b.Name == ComboDiskBand.Text).Id;
            int genreId = db.Genres.FirstOrDefault(b => b.Name == ComboDiskGenre.Text).Id;
            int publisherId = db.Publishers.FirstOrDefault(b => b.Name == ComboDiskPublisher.Text).Id;

            Disk disk = new Disk()
            {
                Name = BoxDiscName.Text,
                BandId = bandId,
                GenreId = genreId,
                PublisherId = publisherId,
                PublishYear = Convert.ToInt32(BoxDiskYear.Text),
                SelfPrice = Convert.ToDecimal(BoxDiskMoney.Text)
            };
            db.Disks.Add(disk);
            db.SaveChanges();
            int diskId = db.Disks.FirstOrDefault(d => d.Name == BoxDiscName.Text && d.BandId == bandId).Id;
            Storage storage = new Storage()
            {
                DiskId = diskId,
                Amount = Convert.ToInt32(BoxStorageAmount.Text)
            };
            db.Storages.Add(storage);

            Price price = new Price()
            {
                DiskId = diskId,
                Percent = Convert.ToDouble(BoxPricePercent.Text)
            };
            db.Prices.Add(price);

            db.SaveChanges();
            LoadDisks();
        }

        private void BtnDiskEdit_Click(object sender, RoutedEventArgs e)
        {
            DiskView diskView = musicListView.SelectedItem as DiskView;

            int bandId = db.Bands.FirstOrDefault(b => b.Name == ComboDiskBand.Text).Id;
            int genreId = db.Genres.FirstOrDefault(b => b.Name == ComboDiskGenre.Text).Id;
            int publisherId = db.Publishers.FirstOrDefault(b => b.Name == ComboDiskPublisher.Text).Id;

            db.Disks.FirstOrDefault(d => d.Id == diskView.DiskId).Name = BoxDiscName.Text;
            db.Disks.FirstOrDefault(d => d.Id == diskView.DiskId).BandId = bandId;
            db.Disks.FirstOrDefault(d => d.Id == diskView.DiskId).GenreId = genreId;
            db.Disks.FirstOrDefault(d => d.Id == diskView.DiskId).PublisherId = publisherId;
            db.Disks.FirstOrDefault(d => d.Id == diskView.DiskId).PublishYear = Convert.ToInt32(BoxDiskYear.Text);
            db.Disks.FirstOrDefault(d => d.Id == diskView.DiskId).SelfPrice = Convert.ToDecimal(BoxDiskMoney.Text);

            db.Storages.FirstOrDefault(s => s.DiskId == diskView.DiskId).Amount = Convert.ToInt32(BoxStorageAmount.Text);

            db.Prices.FirstOrDefault(p => p.DiskId == diskView.DiskId).Percent = Convert.ToDouble(BoxPricePercent.Text);

            db.SaveChanges();
            LoadDisks();
        }

        private void BtnDiskFind_Click(object sender, RoutedEventArgs e)
        {
            musicListView.Items.Clear();

            DiskView d = null;
            double percent = 0;
            decimal money = 0;
            foreach (Disk disk in db.Disks.Where(c => c.Name == BoxDiscName.Text))
            {
                d = new DiskView();

                d.DiskId = disk.Id;
                d.Name = disk.Name;
                d.BandName = db.Bands.FirstOrDefault(p => p.Id == disk.BandId).Name;
                d.BandId = disk.BandId;

                d.GenreName = db.Genres.FirstOrDefault(p => p.Id == disk.GenreId).Name;
                d.GenreId = disk.GenreId;

                d.PublisherName = db.Publishers.FirstOrDefault(p => p.Id == disk.PublisherId).Name;
                d.PublisherId = disk.PublisherId;

                d.PublishYear = disk.PublishYear;

                percent = db.Prices.FirstOrDefault(p => p.DiskId == disk.Id).Percent / 100.0;
                money = disk.SelfPrice + disk.SelfPrice * Convert.ToDecimal(percent);
                d.Money = money;

                d.DiskSelfPrice = disk.SelfPrice;
                d.Amount = db.Storages.FirstOrDefault(s => s.DiskId == disk.Id).Amount;

                musicListView.Items.Add(d);
            }



        }

        private void BtnDiskClear_Click(object sender, RoutedEventArgs e)
        {
            BoxDiscName.Clear();
            BoxDiskMoney.Clear();
            BoxDiskYear.Clear();
            BoxPricePercent.Clear();
            BoxStorageAmount.Clear();

            ComboDiskBand.Text = "";
            ComboDiskFilter.Text = "";
            ComboDiskGenre.Text = "";
            ComboDiskPublisher.Text = "";

            LoadDisks();
        }
    }
}
