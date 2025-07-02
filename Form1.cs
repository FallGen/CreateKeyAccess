using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace CreateKeyAccess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            loadForm();
        }

        private void loadForm()
        {
            textBox1.Text = "СОТРУДНИК МАУ \"ЛДН\"";
            textBox10.Text = "ПОЧЁТНЫЙ ГОСТЬ";
            textBox6.Text = "КОНТРОЛЁР";
            label15.Left = (panel3.Width - label15.Width) / 2;
            label14.Left = (panel6.Width - label14.Width) / 2;
            label1.Left = (panel1.Width - label1.Width) / 2;

            comboBox1.Items.Clear();
            foreach (string printer in PrinterSettings.InstalledPrinters)
                comboBox1.Items.Add(printer);

        }

        private void btnScreen1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PNG Image|*.png";
                    saveFileDialog.Title = "Save Screenshot";
                    saveFileDialog.FileName = "лицо.png";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Центрируем только label1 по горизонтали
                        Label label1 = panel1.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Name == "label1");
                        if (label1 != null)
                        {
                            label1.Location = new Point((panel1.Width - label1.Width) / 2, label1.Location.Y);
                            label1.TextAlign = ContentAlignment.MiddleCenter;
                        }

                        // Создаем битмап для всей панели
                        Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height);

                        // Сначала рисуем фон панели
                        panel1.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, panel1.Width, panel1.Height));

                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            //// Включаем сглаживание






                            // Затем рисуем фотографию сотрудника (PictureBox)
                            PictureBox employeePictureBox = panel1.Controls.OfType<PictureBox>().FirstOrDefault(pbx => pbx.Name == "pictureBox3");
                            if (employeePictureBox != null && employeePictureBox.Image != null)
                            {
                                Rectangle srcRect = new Rectangle(0, 0, employeePictureBox.Image.Width, employeePictureBox.Image.Height);
                                Rectangle destRect = new Rectangle(employeePictureBox.Location, employeePictureBox.Size);
                                g.DrawImage(employeePictureBox.Image, destRect, srcRect, GraphicsUnit.Pixel);
                            }

                            // Сначала рисуем печать (дополнительный PictureBox) выше фотографии
                            PictureBox stampPictureBox = panel1.Controls.OfType<PictureBox>().FirstOrDefault(pbx => pbx.Name == "stampPictureBox");
                            if (stampPictureBox != null && stampPictureBox.Image != null)
                            {
                                Rectangle srcRect = new Rectangle(0, 0, stampPictureBox.Image.Width, stampPictureBox.Image.Height);
                                Rectangle destRect = new Rectangle(stampPictureBox.Location, stampPictureBox.Size);
                                g.DrawImage(stampPictureBox.Image, destRect, srcRect, GraphicsUnit.Pixel);
                            }
                            // Затем рисуем все Label поверх
                            foreach (Control ctrl in panel1.Controls)
                            {
                                if (ctrl is Label label)
                                {
                                    g.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), label.Location);
                                }
                            }
                        }

                        bitmap.Save(saveFileDialog.FileName, ImageFormat.Png);
                        Process.Start(saveFileDialog.FileName);
                    }
                }
            }
            catch
            {
                MessageBox.Show("ошибка");
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    pictureBox3.Image = Image.FromFile(openFileDialog1.FileName);
            }
            catch (Exception E) { MessageBox.Show(E.Message); }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = textBox1.Text;
            label1.Left = (panel1.Width - label1.Width) / 2;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label2.Text = textBox2.Text;
        }

        private void label3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            label4.Text = textBox4.Text;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            label5.Text = textBox5.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            label3.Text = textBox3.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PNG Image|*.png";
                    saveFileDialog.Title = "Save Screenshot";
                    saveFileDialog.FileName = "зад общий.png";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                        pictureBox1.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height - 2));
                        bitmap.Save(saveFileDialog.FileName, ImageFormat.Png);
                        Process.Start(saveFileDialog.FileName);
                    }
                }
            }
            catch { }

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            label15.Text = textBox10.Text;
            label15.Left = (panel3.Width - label15.Width) / 2;
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

            label14.Text = textBox6.Text;
            label14.Left = (panel6.Width - label14.Width) / 2;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PNG Image|*.png";
                    saveFileDialog.Title = "Save Screenshot";
                    saveFileDialog.FileName = textBox6.Text + " зад правила.png";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Центрируем только label1 по горизонтали
                        Label label1 = panel6.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Name == "label14");
                        if (label1 != null)
                        {
                            label1.Location = new Point((panel6.Width - label1.Width) / 2, label1.Location.Y);
                            label1.TextAlign = ContentAlignment.MiddleCenter;
                        }

                        // Создаем битмап для всей панели
                        Bitmap bitmap = new Bitmap(panel6.Width, panel6.Height);

                        // Сначала рисуем фон панели
                        panel6.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, panel6.Width, panel6.Height));

                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            // Включаем сглаживание






                            // Затем рисуем фотографию сотрудника (PictureBox)
                            PictureBox employeePictureBox = panel6.Controls.OfType<PictureBox>().FirstOrDefault(pbx => pbx.Name == "pictureBox3");
                            if (employeePictureBox != null && employeePictureBox.Image != null)
                            {
                                Rectangle srcRect = new Rectangle(0, 0, employeePictureBox.Image.Width, employeePictureBox.Image.Height);
                                Rectangle destRect = new Rectangle(employeePictureBox.Location, employeePictureBox.Size);
                                g.DrawImage(employeePictureBox.Image, destRect, srcRect, GraphicsUnit.Pixel);
                            }

                            // Сначала рисуем печать (дополнительный PictureBox) выше фотографии
                            PictureBox stampPictureBox = panel6.Controls.OfType<PictureBox>().FirstOrDefault(pbx => pbx.Name == "stampPictureBox");
                            if (stampPictureBox != null && stampPictureBox.Image != null)
                            {
                                Rectangle srcRect = new Rectangle(0, 0, stampPictureBox.Image.Width, stampPictureBox.Image.Height);
                                Rectangle destRect = new Rectangle(stampPictureBox.Location, stampPictureBox.Size);
                                g.DrawImage(stampPictureBox.Image, destRect, srcRect, GraphicsUnit.Pixel);
                            }
                            // Затем рисуем все Label поверх
                            foreach (Control ctrl in panel6.Controls)
                            {
                                if (ctrl is Label label)
                                {
                                    g.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), label.Location);
                                }
                            }
                        }

                        bitmap.Save(saveFileDialog.FileName, ImageFormat.Png);
                        Process.Start(saveFileDialog.FileName);
                    }
                }
            }
            catch
            {
                MessageBox.Show("ошибка");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PNG Image|*.png";
                    saveFileDialog.Title = "Save Screenshot";
                    saveFileDialog.FileName = textBox6.Text + " зад карточка лицо.png";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Центрируем только label1 по горизонтали
                        Label label1 = panel3.Controls.OfType<Label>().FirstOrDefault(lbl => lbl.Name == "label15");
                        if (label1 != null)
                        {
                            label1.Location = new Point((panel3.Width - label1.Width) / 2, label1.Location.Y);
                            label1.TextAlign = ContentAlignment.MiddleCenter;
                        }

                        // Создаем битмап для всей панели
                        Bitmap bitmap = new Bitmap(panel3.Width, panel3.Height);

                        // Сначала рисуем фон панели
                        panel3.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, panel3.Width, panel3.Height));

                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            // Включаем сглаживание






                            // Затем рисуем фотографию сотрудника (PictureBox)
                            PictureBox employeePictureBox = panel3.Controls.OfType<PictureBox>().FirstOrDefault(pbx => pbx.Name == "pictureBox3");
                            if (employeePictureBox != null && employeePictureBox.Image != null)
                            {
                                Rectangle srcRect = new Rectangle(0, 0, employeePictureBox.Image.Width, employeePictureBox.Image.Height);
                                Rectangle destRect = new Rectangle(employeePictureBox.Location, employeePictureBox.Size);
                                g.DrawImage(employeePictureBox.Image, destRect, srcRect, GraphicsUnit.Pixel);
                            }

                            // Сначала рисуем печать (дополнительный PictureBox) выше фотографии
                            PictureBox stampPictureBox = panel3.Controls.OfType<PictureBox>().FirstOrDefault(pbx => pbx.Name == "stampPictureBox");
                            if (stampPictureBox != null && stampPictureBox.Image != null)
                            {
                                Rectangle srcRect = new Rectangle(0, 0, stampPictureBox.Image.Width, stampPictureBox.Image.Height);
                                Rectangle destRect = new Rectangle(stampPictureBox.Location, stampPictureBox.Size);
                                g.DrawImage(stampPictureBox.Image, destRect, srcRect, GraphicsUnit.Pixel);
                            }
                            // Затем рисуем все Label поверх
                            foreach (Control ctrl in panel3.Controls)
                            {
                                if (ctrl is Label label)
                                {
                                    g.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), label.Location);
                                }
                            }
                        }

                        bitmap.Save(saveFileDialog.FileName, ImageFormat.Png);
                        Process.Start(saveFileDialog.FileName);
                    }
                }
            }
            catch
            {
                MessageBox.Show("ошибка");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "PNG Image|*.png";
                    saveFileDialog.Title = "Save Screenshot";
                    saveFileDialog.FileName = "почетный гость зад.png";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Bitmap bitmap = new Bitmap(pictureBox4.Width, pictureBox4.Height);
                        pictureBox4.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, pictureBox4.Width, pictureBox4.Height - 2));
                        bitmap.Save(saveFileDialog.FileName, ImageFormat.Png);
                        Process.Start(saveFileDialog.FileName);
                    }
                }
            }
            catch { }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings.PrinterName = comboBox1.SelectedItem.ToString();
            printDocument.DefaultPageSettings.Landscape = true;
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage1);

            try
            {
                printDocument.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка печати: " + ex.Message);
            }
        }

        private void PrintDocument_PrintPage1(object sender, PrintPageEventArgs e)
        {
            // Создайте битмап для панели, чтобы отпечатать её содержимое
            Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height - 2);

            // Сначала рисуем фон панели
            panel1.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, panel1.Width, panel1.Height));

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Включаем сглаживание
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                // Сначала рисуем каждый PictureBox, кроме печати
                foreach (Control ctrl in panel1.Controls)
                {
                    if (ctrl is PictureBox pictureBox && pictureBox.Name != "stampPictureBox")
                    {
                        if (pictureBox.Image != null)
                        {
                            Rectangle srcRect = new Rectangle(0, 0, pictureBox.Image.Width, pictureBox.Image.Height);
                            Rectangle destRect = new Rectangle(pictureBox.Location, pictureBox.Size);
                            g.DrawImage(pictureBox.Image, destRect, srcRect, GraphicsUnit.Pixel);
                        }
                    }
                }

                // Рисуем печать (дополнительный PictureBox)
                PictureBox stampPictureBox = panel1.Controls.OfType<PictureBox>().FirstOrDefault(pbx => pbx.Name == "stampPictureBox");
                if (stampPictureBox != null && stampPictureBox.Image != null)
                {
                    Rectangle srcRect = new Rectangle(0, 0, stampPictureBox.Image.Width, stampPictureBox.Image.Height);
                    Rectangle destRect = new Rectangle(stampPictureBox.Location, stampPictureBox.Size);
                    g.DrawImage(stampPictureBox.Image, destRect, srcRect, GraphicsUnit.Pixel);
                }

                // Затем рисуем все Label поверх
                foreach (Control ctrl in panel1.Controls)
                {
                    if (ctrl is Label label)
                    {
                        g.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), label.Location);
                    }
                }
            }

            float cardWidthInInches = 3.37f; float cardHeightInInches = 2.13f;
                        float bleedSizeInInches = 0.025f; 
            cardWidthInInches += 2 * bleedSizeInInches; cardHeightInInches += 2 * bleedSizeInInches; 
            int cardWidthInGraphics = (int)(cardWidthInInches * 100); 
            int cardHeightInGraphics = (int)(cardHeightInInches * 100); 
            float scale = Math.Min((float)cardWidthInGraphics / bitmap.Width, (float)cardHeightInGraphics / bitmap.Height); 
            int finalWidth = (int)(bitmap.Width * scale); 
            int finalHeight = (int)(bitmap.Height * scale); 
            int x = (e.PageBounds.Width - finalWidth) / 2; 
            int y = (e.PageBounds.Height - finalHeight) / 2; 
            Rectangle destRect1 = new Rectangle(x, y, finalWidth, finalHeight); 
            e.Graphics.DrawImage(bitmap, destRect1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings.PrinterName = comboBox1.SelectedItem.ToString();
            printDocument.DefaultPageSettings.Landscape = true;
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage2);

            try
            {
                printDocument.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка печати: " + ex.Message);
            }

        }

        private void PrintDocument_PrintPage2(object sender, PrintPageEventArgs e)
        {
            // Создайте битмап для панели, чтобы отпечатать её содержимое
            Bitmap bitmap = new Bitmap(panel3.Width, panel3.Height - 2);

            // Сначала рисуем фон панели
            panel3.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, panel3.Width, panel3.Height));

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Включаем сглаживание
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                // Сначала рисуем каждый PictureBox, кроме печати
                foreach (Control ctrl in panel3.Controls)
                {
                    if (ctrl is PictureBox pictureBox && pictureBox.Name != "stampPictureBox")
                    {
                        if (pictureBox.Image != null)
                        {
                            Rectangle srcRect = new Rectangle(0, 0, pictureBox.Image.Width, pictureBox.Image.Height);
                            Rectangle destRect = new Rectangle(pictureBox.Location, pictureBox.Size);
                            g.DrawImage(pictureBox.Image, destRect, srcRect, GraphicsUnit.Pixel);
                        }
                    }
                }

                // Рисуем печать (дополнительный PictureBox)
                PictureBox stampPictureBox = panel3.Controls.OfType<PictureBox>().FirstOrDefault(pbx => pbx.Name == "stampPictureBox");
                if (stampPictureBox != null && stampPictureBox.Image != null)
                {
                    Rectangle srcRect = new Rectangle(0, 0, stampPictureBox.Image.Width, stampPictureBox.Image.Height);
                    Rectangle destRect = new Rectangle(stampPictureBox.Location, stampPictureBox.Size);
                    g.DrawImage(stampPictureBox.Image, destRect, srcRect, GraphicsUnit.Pixel);
                }

                // Затем рисуем все Label поверх
                foreach (Control ctrl in panel3.Controls)
                {
                    if (ctrl is Label label)
                    {
                        g.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), label.Location);
                    }
                }
            }

            float cardWidthInInches = 3.37f; float cardHeightInInches = 2.13f;
                        float bleedSizeInInches = 0.025f;
            cardWidthInInches += 2 * bleedSizeInInches; cardHeightInInches += 2 * bleedSizeInInches;
            int cardWidthInGraphics = (int)(cardWidthInInches * 100);
            int cardHeightInGraphics = (int)(cardHeightInInches * 100);
            float scale = Math.Min((float)cardWidthInGraphics / bitmap.Width, (float)cardHeightInGraphics / bitmap.Height);
            int finalWidth = (int)(bitmap.Width * scale);
            int finalHeight = (int)(bitmap.Height * scale);
            int x = (e.PageBounds.Width - finalWidth) / 2;
            int y = (e.PageBounds.Height - finalHeight) / 2;
            Rectangle destRect1 = new Rectangle(x, y, finalWidth, finalHeight);
            e.Graphics.DrawImage(bitmap, destRect1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings.PrinterName = comboBox1.SelectedItem.ToString();
            printDocument.DefaultPageSettings.Landscape = true;

            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage3);

            try
            {
                printDocument.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка печати: " + ex.Message);
            }
        }

        private void PrintDocument_PrintPage3(object sender, PrintPageEventArgs e)
        {
            // Создайте битмап для панели, чтобы отпечатать её содержимое
            Bitmap bitmap = new Bitmap(panel6.Width, panel6.Height - 2);

            // Сначала рисуем фон панели
            panel6.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, panel6.Width, panel6.Height));

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                // Включаем сглаживание
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;

                // Сначала рисуем каждый PictureBox, кроме печати
                foreach (Control ctrl in panel6.Controls)
                {
                    if (ctrl is PictureBox pictureBox && pictureBox.Name != "stampPictureBox")
                    {
                        if (pictureBox.Image != null)
                        {
                            Rectangle srcRect = new Rectangle(0, 0, pictureBox.Image.Width, pictureBox.Image.Height);
                            Rectangle destRect = new Rectangle(pictureBox.Location, pictureBox.Size);
                            g.DrawImage(pictureBox.Image, destRect, srcRect, GraphicsUnit.Pixel);
                        }
                    }
                }

                // Рисуем печать (дополнительный PictureBox)
                PictureBox stampPictureBox = panel6.Controls.OfType<PictureBox>().FirstOrDefault(pbx => pbx.Name == "stampPictureBox");
                if (stampPictureBox != null && stampPictureBox.Image != null)
                {
                    Rectangle srcRect = new Rectangle(0, 0, stampPictureBox.Image.Width, stampPictureBox.Image.Height);
                    Rectangle destRect = new Rectangle(stampPictureBox.Location, stampPictureBox.Size);
                    g.DrawImage(stampPictureBox.Image, destRect, srcRect, GraphicsUnit.Pixel);
                }

                // Затем рисуем все Label поверх
                foreach (Control ctrl in panel6.Controls)
                {
                    if (ctrl is Label label)
                    {
                        g.DrawString(label.Text, label.Font, new SolidBrush(label.ForeColor), label.Location);
                    }
                }
            }

            float cardWidthInInches = 3.37f; float cardHeightInInches = 2.13f;
                        float bleedSizeInInches = 0.025f;
            cardWidthInInches += 2 * bleedSizeInInches; cardHeightInInches += 2 * bleedSizeInInches;
            int cardWidthInGraphics = (int)(cardWidthInInches * 100);
            int cardHeightInGraphics = (int)(cardHeightInInches * 100);
            float scale = Math.Min((float)cardWidthInGraphics / bitmap.Width, (float)cardHeightInGraphics / bitmap.Height);
            int finalWidth = (int)(bitmap.Width * scale);
            int finalHeight = (int)(bitmap.Height * scale);
            int x = (e.PageBounds.Width - finalWidth) / 2;
            int y = (e.PageBounds.Height - finalHeight) / 2;
            Rectangle destRect1 = new Rectangle(x, y, finalWidth, finalHeight);
            e.Graphics.DrawImage(bitmap, destRect1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings.PrinterName = comboBox1.SelectedItem.ToString();
            printDocument.DefaultPageSettings.Landscape = true;

            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage4);

            try
            {
                printDocument.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка печати: " + ex.Message);
            }
        }

        private void PrintDocument_PrintPage4(object sender, PrintPageEventArgs e)
        {
            // Создайте битмап для PictureBox и нарисуйте его содержимое
            Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bitmap, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));

            float cardWidthInInches = 3.37f; float cardHeightInInches = 2.13f;
                        float bleedSizeInInches = 0.025f;
            cardWidthInInches += 2 * bleedSizeInInches; cardHeightInInches += 2 * bleedSizeInInches;
            int cardWidthInGraphics = (int)(cardWidthInInches * 100);
            int cardHeightInGraphics = (int)(cardHeightInInches * 100);
            float scale = Math.Min((float)cardWidthInGraphics / bitmap.Width, (float)cardHeightInGraphics / bitmap.Height);
            int finalWidth = (int)(bitmap.Width * scale);
            int finalHeight = (int)(bitmap.Height * scale);
            int x = (e.PageBounds.Width - finalWidth) / 2;
            int y = (e.PageBounds.Height - finalHeight) / 2;
            Rectangle destRect1 = new Rectangle(x, y, finalWidth, finalHeight);
            e.Graphics.DrawImage(bitmap, destRect1);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrinterSettings.PrinterName = comboBox1.SelectedItem.ToString();
            printDocument.DefaultPageSettings.Landscape = true;

            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage5);

            try
            {
                printDocument.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка печати: " + ex.Message);
            }
        }

        private void PrintDocument_PrintPage5(object sender, PrintPageEventArgs e)
        {
            // Создайте битмап для панели, чтобы отпечатать её содержимое
            Bitmap bitmap = new Bitmap(pictureBox4.Width, pictureBox4.Height - 2);
            pictureBox4.DrawToBitmap(bitmap, new Rectangle(0, 0, pictureBox4.Width, pictureBox4.Height));
            float cardWidthInInches = 3.37f; float cardHeightInInches = 2.13f;
                        float bleedSizeInInches = 0.025f;
            cardWidthInInches += 2 * bleedSizeInInches; 
            cardHeightInInches += 2 * bleedSizeInInches;
            int cardWidthInGraphics = (int)(cardWidthInInches * 100);
            int cardHeightInGraphics = (int)(cardHeightInInches * 100);
            float scale = Math.Min((float)cardWidthInGraphics / bitmap.Width, (float)cardHeightInGraphics / bitmap.Height);
            int finalWidth = (int)(bitmap.Width * scale);
            int finalHeight = (int)(bitmap.Height * scale);
            int x = (e.PageBounds.Width - finalWidth) / 2;
            int y = (e.PageBounds.Height - finalHeight) / 2;
            Rectangle destRect1 = new Rectangle(x, y, finalWidth, finalHeight);
            e.Graphics.DrawImage(bitmap, destRect1);
        }





        /*
         

        private void button2_Click(object sender, EventArgs e)
{
    PrintDocument printDocument = new PrintDocument();

    // Установите выбранный принтер
    printDocument.PrinterSettings.PrinterName = comboBox1.SelectedItem.ToString();

    // Установите поля страницы (однородные поля по 1 мм)
    printDocument.DefaultPageSettings.Margins = new Margins(1, 1, 1, 1);

    // Установите альбомную ориентацию
    printDocument.DefaultPageSettings.Landscape = true;

    printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage4);

    try
    {
        printDocument.Print();
    }
    catch (Exception ex)
    {
        MessageBox.Show("Ошибка печати: " + ex.Message);
    }
}

private void PrintDocument_PrintPage4(object sender, PrintPageEventArgs e)
{
    // Создайте битмап для PictureBox и нарисуйте его содержимое
    Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
    pictureBox1.DrawToBitmap(bitmap, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));

    // Размер карты: 85.6 мм x 54 мм (примерно 3.37 дюйма x 2.13 дюйма)
    float cardWidthInInches = 3.37f;
    float cardHeightInInches = 2.13f;

    // Получим размеры карты в единицах графики (1 дюйм = 100 единиц графики)
    int cardWidthInGraphics = (int)(cardWidthInInches * 100);
    int cardHeightInGraphics = (int)(cardHeightInInches * 100);

    // Подберите размер изображения, чтобы оно соответствовало размеру карты
    float scale = Math.Min((float)cardWidthInGraphics / bitmap.Width, (float)cardHeightInGraphics / bitmap.Height);
    int finalWidth = (int)(bitmap.Width * scale);
    int finalHeight = (int)(bitmap.Height * scale);

    // Центрируем изображение на странице
    int x = (e.PageBounds.Width - finalWidth) / 2;
    int y = (e.PageBounds.Height - finalHeight) / 2;

    Rectangle destRect = new Rectangle(x, y, finalWidth, finalHeight);

    // Печатаем битмап на карту с учетом масштаба
    e.Graphics.DrawImage(bitmap, destRect);
}

private void button9_Click(object sender, EventArgs e)
{
    PrintDocument printDocument = new PrintDocument();

    // Установите выбранный принтер
    printDocument.PrinterSettings.PrinterName = comboBox1.SelectedItem.ToString();

    // Установите поля страницы (однородные поля по 1 мм)
    printDocument.DefaultPageSettings.Margins = new Margins(1, 1, 1, 1);

    // Установите альбомную ориентацию
    printDocument.DefaultPageSettings.Landscape = true;

    printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage5);

    try
    {
        printDocument.Print();
    }
    catch (Exception ex)
    {
        MessageBox.Show("Ошибка печати: " + ex.Message);
    }
}

private void PrintDocument_PrintPage5(object sender, PrintPageEventArgs e)
{
    // Создайте битмап для PictureBox и нарисуйте его содержимое
    Bitmap bitmap = new Bitmap(pictureBox4.Width, pictureBox4.Height);
    pictureBox4.DrawToBitmap(bitmap, new Rectangle(0, 0, pictureBox4.Width, pictureBox4.Height));

    // Размер карты: 85.6 мм x 54 мм (примерно 3.37 дюйма x 2.13 дюйма)
    float cardWidthInInches = 3.37f;
    float cardHeightInInches = 2.13f;

    // Получим размеры карты в единицах графики (1 дюйм = 100 единиц графики)
    int cardWidthInGraphics = (int)(cardWidthInInches * 100);
    int cardHeightInGraphics = (int)(cardHeightInInches * 100);

    // Подберите размер изображения, чтобы оно соответствовало размеру карты
    float scale = Math.Min((float)cardWidthInGraphics / bitmap.Width, (float)cardHeightInGraphics / bitmap.Height);
    int finalWidth = (int)(bitmap.Width * scale);
    int finalHeight = (int)(bitmap.Height * scale);

    // Центрируем изображение на странице
    int x = (e.PageBounds.Width - finalWidth) / 2;
    int y = (e.PageBounds.Height - finalHeight) / 2;

    Rectangle destRect = new Rectangle(x, y, finalWidth, finalHeight);

    // Печатаем битмап на карту с учетом масштаба
    e.Graphics.DrawImage(bitmap, destRect);
}


         private void button2_Click(object sender, EventArgs e)
{
    PrintDocument printDocument = new PrintDocument();

    // Установите выбранный принтер
    printDocument.PrinterSettings.PrinterName = comboBox1.SelectedItem.ToString();

    // Установите поля страницы (однородные поля по 1 мм)
    printDocument.DefaultPageSettings.Margins = new Margins(1, 1, 1, 1);

    // Установите альбомную ориентацию
    printDocument.DefaultPageSettings.Landscape = true;

    printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage4);

    try
    {
        printDocument.Print();
    }
    catch (Exception ex)
    {
        MessageBox.Show("Ошибка печати: " + ex.Message);
    }
}

private void PrintDocument_PrintPage4(object sender, PrintPageEventArgs e)
{
    // Создайте битмап для PictureBox и нарисуйте его содержимое
    Bitmap bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
    pictureBox1.DrawToBitmap(bitmap, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));

    // Размер карты: 85.6 мм x 54 мм (примерно 3.37 дюйма x 2.13 дюйма)
    int cardWidth = (int)(3.37 * 100);
    int cardHeight = (int)(2.13 * 100);

    // Убедитесь, что размеры учитывают ориентацию страницы
    int scaledWidth, scaledHeight;
    if (e.PageSettings.Landscape)
    {
        scaledWidth = Math.Min(cardHeight, cardWidth);
        scaledHeight = Math.Max(cardHeight, cardWidth);
    }
    else
    {
        scaledWidth = Math.Max(cardHeight, cardWidth);
        scaledHeight = Math.Min(cardHeight, cardWidth);
    }

    // Подберите размер изображения, чтобы оно соответствовало размеру карты
    float scale = Math.Min((float)scaledWidth / bitmap.Width, (float)scaledHeight / bitmap.Height);
    int finalWidth = (int)(bitmap.Width * scale);
    int finalHeight = (int)(bitmap.Height * scale);

    // Центрируем изображение на странице
    int x = (e.PageBounds.Width - finalWidth) / 2;
    int y = (e.PageBounds.Height - finalHeight) / 2;

    Rectangle destRect = new Rectangle(x, y, finalWidth, finalHeight);

    // Печатаем битмап на карту с учетом масштаба
    e.Graphics.DrawImage(bitmap, destRect);
}

private void button9_Click(object sender, EventArgs e)
{
    PrintDocument printDocument = new PrintDocument();

    // Установите выбранный принтер
    printDocument.PrinterSettings.PrinterName = comboBox1.SelectedItem.ToString();

    // Установите поля страницы (однородные поля по 1 мм)
    printDocument.DefaultPageSettings.Margins = new Margins(1, 1, 1, 1);

    // Установите альбомную ориентацию
    printDocument.DefaultPageSettings.Landscape = true;

    printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage5);

    try
    {
        printDocument.Print();
    }
    catch (Exception ex)
    {
        MessageBox.Show("Ошибка печати: " + ex.Message);
    }
}

private void PrintDocument_PrintPage5(object sender, PrintPageEventArgs e)
{
    // Создайте битмап для PictureBox и нарисуйте его содержимое
    Bitmap bitmap = new Bitmap(pictureBox4.Width, pictureBox4.Height);
    pictureBox4.DrawToBitmap(bitmap, new Rectangle(0, 0, pictureBox4.Width, pictureBox4.Height));

    // Размер карты: 85.6 мм x 54 мм (примерно 3.37 дюйма x 2.13 дюйма)
    int cardWidth = (int)(3.37 * 100);
    int cardHeight = (int)(2.13 * 100);

    // Убедитесь, что размеры учитывают ориентацию страницы
    int scaledWidth, scaledHeight;
    if (e.PageSettings.Landscape)
    {
        scaledWidth = Math.Min(cardHeight, cardWidth);
        scaledHeight = Math.Max(cardHeight, cardWidth);
    }
    else
    {
        scaledWidth = Math.Max(cardHeight, cardWidth);
        scaledHeight = Math.Min(cardHeight, cardWidth);
    }

    // Подберите размер изображения, чтобы оно соответствовало размеру карты
    float scale = Math.Min((float)scaledWidth / bitmap.Width, (float)scaledHeight / bitmap.Height);
    int finalWidth = (int)(bitmap.Width * scale);
    int finalHeight = (int)(bitmap.Height * scale);

    // Центрируем изображение на странице
    int x = (e.PageBounds.Width - finalWidth) / 2;
    int y = (e.PageBounds.Height - finalHeight) / 2;

    Rectangle destRect = new Rectangle(x, y, finalWidth, finalHeight);

    // Печатаем битмап на карту с учетом масштаба
    e.Graphics.DrawImage(bitmap, destRect);
}
 
          
         */
    }
}
