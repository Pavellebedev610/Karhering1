using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using System;
using System.Windows.Forms;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using System.Data.Entity;
using Guna.UI2.WinForms;
using Microsoft.VisualBasic.Logging;
using System.Diagnostics.Eventing.Reader;
using System.Data.SqlClient;
using System.Data;
using Karhering.Repository;
using System.Reflection.Emit;
using System.Management;
using System.Data.Entity.Core.Mapping;
using System.Drawing;
using System.Text;
using Dapper;
using static Karhering.Repository.ClientRepos;
using Microsoft.VisualBasic.ApplicationServices;
using static GMap.NET.Entity.OpenStreetMapRouteEntity;


namespace Karhering
{
    public partial class Hub : Form
    {
        private Bitmap resizedImage;
        //private Timer timer;
        public Client ClientInfo { get; set; }
        public int UserId { get; set; }
        public Hub()
        {
            InitializeComponent();
            label6.Text = DataBank.Text;
            Load += new EventHandler(Hub_Load);
            _obj = this;

        }
        static Hub _obj;
        public static Hub Instance
        {
            get
            {
                if (_obj == null)
                {
                    _obj = new Hub();
                }
                return _obj;
            }
        }
        public Panel pnlAdmin
        {
            get { return AdminPanel; }
            set { AdminPanel = value; }
        }
        public Panel pnlUser
        {
            get { return UserPanel; }
            set { UserPanel = value; }
        }


        System.Timers.Timer Timer;
        System.Timers.Timer Timer2;
        System.Timers.Timer Timer3;
        int c, m, s;
        int d = 60;
        int v = 14;

        private Car curentCar;
        private void gMapControl1_Load(object sender, EventArgs e)
        {
            gMapControl1.MapProvider = GoogleMapProvider.Instance;
            gMapControl1.CacheLocation = Application.StartupPath + @"\maps\OSMCache";
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gMapControl1.CanDragMap = true;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MouseWheelZoomEnabled = true;
            gMapControl1.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;
            gMapControl1.MinZoom = 10;
            gMapControl1.MaxZoom = 20;
            gMapControl1.Zoom = 15;
            gMapControl1.Position = new PointLatLng(59.9386, 30.3141);
            gMapControl1.ShowCenter = false;
            Createmarker();
        }

        private GMapOverlay ListOfCar; // Объявляем переменную для оверлея ListOfCar на уровне класса

        private void Createmarker()
        {
            if (ListOfCar != null)
            {
                // Удаляем предыдущие маркеры перед созданием новых
                gMapControl1.Overlays.Remove(ListOfCar);
            }

            ListOfCar = new GMapOverlay("Car");

            Bitmap originalImage = new Bitmap(@"C:\Users\Павел\source\repos\Karhering\Karhering\Icon\car.png");
            int newWidth = (int)(originalImage.Width * 0.03);
            int newHeight = (int)(originalImage.Height * 0.03);
            resizedImage = new Bitmap(originalImage, new Size(newWidth, newHeight));

            var repos = new CarRepos();
            var cars = repos.GetCars();

            foreach (var coordinat in cars)
            {
                var marker = new CarMarker(new PointLatLng((double)coordinat.cordinat_x, (double)coordinat.cordinat_y), resizedImage);
                marker.ToolTip = new GMapRoundedToolTip(marker);
                marker.CarInfo = coordinat;

                ListOfCar.Markers.Add(marker);
            }

            GMarkerGoogle marker11 = new GMarkerGoogle(new PointLatLng(60.023826, 30.228222), GMarkerGoogleType.red_small);
            marker11.ToolTip = new GMapRoundedToolTip(marker11);
            marker11.ToolTipText = "Мое место положение";
            ListOfCar.Markers.Add(marker11);

            gMapControl1.Overlays.Add(ListOfCar);
            gMapControl1.OnMarkerClick += GMapControl1_OnMarkerClick;
            gMapControl1.OnMapClick += GMapControl1_OnMapClick;
        }

        public class CarMarker : GMarkerGoogle
        {
            public CarMarker(PointLatLng p, Bitmap bitmap) : base(p, bitmap)
            {

            }
            public Car CarInfo { get; set; }

            public Bitmap image { get; set; }
        }
        private GMapMarker selectedMarker = null;
        private Baza _db;

        private void GMapControl1_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {

            CarInfo.Visible = false;
            if (item is not CarMarker car)
            {
                return;

            }
            CarInfo.Visible = true;
            CarNumber.Text = car.CarInfo.marka_auto;
            label30.Text = car.CarInfo.model_auto;
            label26.Text = car.CarInfo.toplivo;
            label24.Text = car.CarInfo.probeg;
            label22.Text = car.CarInfo.number;
            GAS.Text = car.CarInfo.toplivo;
            Probeg.Text = car.CarInfo.probeg;
            Number.Text = car.CarInfo.number;
            label17.Text = car.CarInfo.number;
            GAS2.Text = car.CarInfo.toplivo;
            label32.Text = car.CarInfo.number;
            CarImage.Image = car.image;
            CarImage10.Image = car.image;
            CarImage4.Image = car.image;
            CarImage2.Image = car.image;
            CarName.Text = car.CarInfo.marka_auto;
            label33.Text = car.CarInfo.model_auto;
            label27.Text = car.CarInfo.model_auto;
            label34.Text = car.CarInfo.marka_auto;
            curentCar = car.CarInfo;

            string photoPath = Encoding.UTF8.GetString(car.CarInfo.PhotoCar);
            LoadCarPhoto(photoPath, CarImage);
            LoadCarPhoto(photoPath, CarImage10);
            LoadCarPhoto(photoPath, CarImage4);
            LoadCarPhoto(photoPath, CarImage2);
            if (item is not CarMarker carMarker)
            {
                return;
            }
            selectedMarker = carMarker;
        }
        private void LoadCarPhoto(string photoPath, PictureBox pictureBox)
        {
            try
            {

                // Загрузка фотографии машины из указанного пути и отображение ее в PictureBox
                using (MemoryStream ms = new MemoryStream(curentCar.PhotoCar))
                {
                    CarImage.Image = Image.FromStream(ms);
                    CarImage10.Image = Image.FromStream(ms);
                    CarImage4.Image = Image.FromStream(ms);
                    CarImage2.Image = Image.FromStream(ms);
                }
            }
            catch (Exception ex)
            {
                // Обработка ошибок, если фотография не может быть загружена
                MessageBox.Show($"Ошибка загрузки фотографии: {ex.Message}");
            }
        }
        private void GMapControl1_OnMapClick(PointLatLng pointClick, MouseEventArgs e)
        {
            CarInfo.Visible = false;

            FullPanel.Visible = false;
            pictureBox6.Visible = true;
            curentCar = null;
            selectedMarker = null;
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Auto log1 = new Auto();
            this.Hide();
            log1.Show();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Poderjka log2 = new Poderjka();
            log2.UserId = this.UserId;
            this.Hide();
            log2.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Nastroiki form = new Nastroiki();
            form.UserId = UserId;
            this.Hide();
            form.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Bonus form = new Bonus();
            form.UserId = UserId;
            this.Hide();
            form.Show();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {
            CarInfo.Visible = false;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Minutes.Visible = !Minutes.Visible;
            hidePanels();
            Minutes.Visible = true;


        }

        private void guna2Button6_Click_1(object sender, EventArgs e)
        {
            Auto log = new Auto();
            log.UserId = this.UserId;
            this.Hide();
            log.Show();
        }


        public void hidePanels()
        {
            Minutes.Visible = false;
            CarInfo.Visible = false;
            ArendaBron.Visible = false;
            FinalPrice.Visible = false;
            Arenda.Visible = false;
            Finish.Visible = false;
            HoursePanel.Visible = false;
            Days.Visible = false;


        }

        private void Hub_Load(object sender, EventArgs e)
        {
            hidePanels();
            ojid.Visible = false;
            ButtonGo.Visible = false;
            label15.Text = DateTime.Now.ToString("dd.MM.yyyy, HH.mm");

            ClientInfo = new ClientRepos().GetUser(UserId);

            if (ClientInfo != null)
            {
                // Если информация о клиенте успешно получена, устанавливаем ФИО на форме
                label6.Text = ClientInfo.FIO;
                label37.Text = ClientInfo.rating;
            }
        }


        private void pictureBox6_Click(object sender, EventArgs e)
        {
            FullPanel.Visible = !FullPanel.Visible;
            hidePanels();
            FullPanel.Visible = true;
            UserPanel.Visible = !UserPanel.Visible;
            hidePanels();
            UserPanel.Visible = true;
            panel15.Visible = !panel15.Visible;
            hidePanels();
            panel15.Visible = true;
            pictureBox6.Visible = false;
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            hidePanels();
            timer4.Stop();
            if (selectedMarker == null)
            {
                // Создаем новый маркер, если его нет
                var repos = new CarRepos();
                var cars = repos.GetCars();

                foreach (var coordinat in cars)
                {
                    var marker = new CarMarker(new PointLatLng((double)coordinat.cordinat_x, (double)coordinat.cordinat_y), resizedImage);
                    marker.ToolTip = new GMapRoundedToolTip(marker);
                    marker.CarInfo = coordinat;

                    ListOfCar.Markers.Add(marker);
                }
            }
        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {
            Arenda.Visible = !Arenda.Visible;
            hidePanels();
            Arenda.Visible = true;
            timer4.Stop();
            // Остановить таймер
            v = 14; // Начальное значение для часов
            d = 59; // Начальное значение для минут
            timerPrice.Start();
            timer1.Start();
            WriteBeginningTimeToDatabase(DateTime.Now);

        }
        private void WriteBeginningTimeToDatabase(DateTime time)
        {
            try
            {
                Baza baza = new Baza();
                baza.openConnection();

                string query = $@"INSERT INTO [dbo].[Arenda]
            ([beginning_time]
            ,[end_time]
            ,[Travel_time]
            ,[car_id]
            ,[id_client])
        VALUES
            (@beginning_time
            ,@end_time
            ,@Travel_time
            ,@car_id
            ,@id_client)";
                using (SqlCommand command = new SqlCommand(query, baza.getConnection()))
                {
                    // Добавляем параметры в команду SQL
                    command.Parameters.AddWithValue("@beginning_time", time);

                    // Выполняем команду SQL
                    command.Parameters.AddWithValue("@end_time", DBNull.Value);
                    command.Parameters.AddWithValue("@Travel_time", DBNull.Value);
                    command.Parameters.AddWithValue("@car_id", curentCar.id_car);
                    command.Parameters.AddWithValue("@id_client", ClientInfo.id_polz);

                    command.ExecuteNonQuery();
                }
                baza.closeConnection();
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Ошибка при записи времени начала аренды в базу данных: {ex.Message}");
            }
        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            ojid.Visible = !ojid.Visible;
            go.Visible = false;
            ojid.Visible = true;

            ButtonOjid.Visible = !ButtonOjid.Visible;
            ButtonGo.Visible = true;
            ButtonOjid.Visible = false;

        }

        private void ButtonGo_Click(object sender, EventArgs e)
        {
            go.Visible = !go.Visible;
            ojid.Visible = false;
            go.Visible = true;

            ButtonGo.Visible = !ButtonGo.Visible;
            ButtonOjid.Visible = true;
            ButtonGo.Visible = false;


        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            ArendaBron.Visible = !ArendaBron.Visible;
            hidePanels();
            ArendaBron.Visible = true;
            timer4.Stop(); // Остановить тайме


            timer4.Start();
            if (selectedMarker != null)
            {
                // Удаляем выбранный маркер с карты
                ListOfCar.Markers.Remove(selectedMarker);
                // Сбрасываем выбранный маркер
                selectedMarker = null;
            }
        }


        private void guna2Button15_Click(object sender, EventArgs e)
        {
            Finish.Visible = !Finish.Visible;
            Finish.Visible = true;
        }

        private void guna2Button5_Click_1(object sender, EventArgs e)
        {
            Finish.Visible = false;
        }

        private void guna2Button14_Click_1(object sender, EventArgs e)
        {
            WriteEndTimeToDatabase(DateTime.Now);
            FinalPrice.Visible = !FinalPrice.Visible;
            hidePanels();
            FinalPrice.Visible = true;
            c = 0;
            m = 0;
            s = 0;
            timer1.Stop();
            ArendaPrice.Text = "0,00";
            timerPrice.Stop();

            if (selectedMarker == null)
            {
                // Создаем новый маркер, если его нет
                var repos = new CarRepos();
                var cars = repos.GetCars();

                foreach (var coordinat in cars)
                {
                    var marker = new CarMarker(new PointLatLng((double)coordinat.cordinat_x, (double)coordinat.cordinat_y), resizedImage);
                    marker.ToolTip = new GMapRoundedToolTip(marker);
                    marker.CarInfo = coordinat;

                    ListOfCar.Markers.Add(marker);
                }
            }
        }

        private void WriteEndTimeToDatabase(DateTime time)
        {
            try
            {
                Baza baza = new Baza();
                baza.openConnection();

                string updateQuery = $@"UPDATE [dbo].[Arenda]
                                SET [end_time] = @end_time
                                WHERE [beginning_time] = (SELECT MAX([beginning_time]) FROM [dbo].[Arenda])";

                using (SqlCommand command = new SqlCommand(updateQuery, baza.getConnection()))
                {
                    // Добавляем параметры в команду SQL
                    command.Parameters.AddWithValue("@end_time", time);
                    command.ExecuteNonQuery();
                }

                // Получаем время начала аренды
                DateTime beginningTime;
                string selectQuery = "SELECT MAX([beginning_time]) FROM [dbo].[Arenda]";
                using (SqlCommand command = new SqlCommand(selectQuery, baza.getConnection()))
                {
                    beginningTime = (DateTime)command.ExecuteScalar();
                }

                // Вычисляем время поездки
                TimeSpan travelTime = time - beginningTime;

                // Обновляем время поездки в базе данных
                string updateTravelTimeQuery = @"UPDATE [dbo].[Arenda]
                                        SET [Travel_time] = @travel_time
                                        WHERE [beginning_time] = @beginning_time";
                using (SqlCommand command = new SqlCommand(updateTravelTimeQuery, baza.getConnection()))
                {
                    command.Parameters.AddWithValue("@travel_time", travelTime);
                    command.Parameters.AddWithValue("@beginning_time", beginningTime);
                    label39.Text = travelTime.ToString(@"hh\:mm\:ss");
                    command.ExecuteNonQuery();
                }
                CalculateTripTimeAndCost();
                baza.closeConnection();
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Ошибка при записи времени окончания аренды в базу данных: {ex.Message}");
            }

        }

        private void CalculateTripTimeAndCost()
        {
            try
            {
                Baza baza = new Baza();
                baza.openConnection();

                // Получаем время начала и окончания аренды
                DateTime beginningTime, endTime;
                string selectQuery = "SELECT MAX([beginning_time]), MAX([end_time]) FROM [dbo].[Arenda]";
                using (SqlCommand command = new SqlCommand(selectQuery, baza.getConnection()))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Read();
                    beginningTime = (DateTime)reader[0];
                    endTime = (DateTime)reader[1];
                    reader.Close();
                }

                // Вычисляем время поездки
                TimeSpan travelTime = endTime - beginningTime;

                // Рассчитываем стоимость поездки (24,17 рубля за минуту)
                double cost = travelTime.TotalMinutes * 24.17;

                // Округляем стоимость до двух знаков после запятой
                cost = Math.Round(cost, 2);

                // Обновляем Label с информацией о времени поездки и стоимости
                label28.Text = $"{cost} руб.";

                // Записываем значение стоимости в базу данных
                string updateQuery = "UPDATE [dbo].[Arenda] SET [cost] = @cost WHERE [end_time] = (SELECT MAX([end_time]) FROM [dbo].[Arenda])";
                using (SqlCommand command = new SqlCommand(updateQuery, baza.getConnection()))
                {
                    command.Parameters.AddWithValue("@cost", cost);
                    command.ExecuteNonQuery();
                }

                baza.closeConnection();
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                MessageBox.Show($"Ошибка при расчете времени поездки и стоимости: {ex.Message}");
            }
        }

        private void guna2Button18_Click(object sender, EventArgs e)
        {
            hidePanels();
        }

        private void guna2Button19_Click(object sender, EventArgs e)
        {
            Trips log = new Trips();
            log.UserId = this.UserId;
            this.Hide();
            log.Show();
        }

        private void guna2Button17_Click(object sender, EventArgs e)
        {
            CreateCar log = new CreateCar();
            log.UserId = this.UserId;
            this.Hide();
            log.Show();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                s = s + 1;
                if (s == 60)
                {
                    m = m + 1;
                    s = 0;
                }

                if (m == 60)
                {
                    c = c + 1;
                    m = 0;
                    s = 0;
                }

                string hours = c.ToString("D2");
                string minutes = m.ToString("D2");
                string seconds = s.ToString("D2");

                ArendaTime.Text = hours + ":" + minutes + ":" + seconds;
            }));
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            Invoke(new Action(() =>
            {
                d = d - 1; // Уменьшаем счетчик на 1 секунду
                if (d < 0)
                {
                    v = v - 1;
                    d = 59;
                }

                string minutes = v.ToString("D2");
                string seconds = d.ToString("D2");

                label36.Text = minutes + ":" + seconds;
            }));
        }

        private void timerPrice_Tick(object sender, EventArgs e)
        {
            double priceIncrement = 0.40;

            // Выполняем обновление элемента управления в потоке UI
            Invoke(new Action(() =>
            {
                double currentPrice = double.Parse(ArendaPrice.Text) + priceIncrement;
                ArendaPrice.Text = currentPrice.ToString("0.00");
            }));
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            HoursePanel.Visible = !HoursePanel.Visible;
            hidePanels();
            HoursePanel.Visible = true;
        }

        private void guna2Button34_Click(object sender, EventArgs e)
        {
            ArendaBron.Visible = !ArendaBron.Visible;
            hidePanels();
            ArendaBron.Visible = true;
            timer4.Stop(); // Остановить тайме


            timer4.Start();
            if (selectedMarker != null)
            {
                // Удаляем выбранный маркер с карты
                ListOfCar.Markers.Remove(selectedMarker);
                // Сбрасываем выбранный маркер
                selectedMarker = null;
            }
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            Days.Visible = !Days.Visible;
            hidePanels();
            Days.Visible = true;
        }

        private void guna2Button48_Click(object sender, EventArgs e)
        {
            ArendaBron.Visible = !ArendaBron.Visible;
            hidePanels();
            ArendaBron.Visible = true;
            timer4.Stop(); // Остановить тайме


            timer4.Start();
            if (selectedMarker != null)
            {
                // Удаляем выбранный маркер с карты
                ListOfCar.Markers.Remove(selectedMarker);
                // Сбрасываем выбранный маркер
                selectedMarker = null;
            }
        }
    }
}
