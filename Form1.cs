using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Buoi07_TinhToan3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtSo1.Text = txtSo2.Text = "0";

            // Thêm event cho các TextBox
            txtSo1.Enter += TextBox_Enter;
            txtSo2.Enter += TextBox_Enter;
            txtKq.Enter += TextBox_Enter;

            txtSo1.MouseDown += TextBox_MouseDown;
            txtSo2.MouseDown += TextBox_MouseDown;
            txtKq.MouseDown += TextBox_MouseDown;
        }

        // Khi focus bằng Tab => bôi đen
        private void TextBox_Enter(object sender, EventArgs e)
        {
            (sender as TextBox)?.SelectAll();
        }

        // Khi click chuột => dùng BeginInvoke để delay
        private void TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null)
            {
                this.BeginInvoke((Action)(() => tb.SelectAll()));
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Bạn có thực sự muốn thoát không?",
                                 "Thông báo", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                this.Close();
        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            //lấy giá trị của 2 ô số
            double so1, so2, kq = 0;
            // Kiểm tra rỗng cho ô số 1
            if (string.IsNullOrWhiteSpace(txtSo1.Text))
            {
                MessageBox.Show("Ô thứ nhất đang để trống. Vui lòng nhập số!",
                                "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSo1.Focus();
                return;
            }

            // Kiểm tra rỗng cho ô số 2
            if (string.IsNullOrWhiteSpace(txtSo2.Text))
            {
                MessageBox.Show("Ô thứ hai đang để trống. Vui lòng nhập số!",
                                "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSo2.Focus();
                return;
            }

            // Kiểm tra dữ liệu có phải số hợp lệ
            if (!double.TryParse(txtSo1.Text, out so1))
            {
                MessageBox.Show("Giá trị ô thứ nhất không hợp lệ. Vui lòng nhập số!",
                                "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSo1.Focus();
                return;
            }

            if (!double.TryParse(txtSo2.Text, out so2))
            {
                MessageBox.Show("Giá trị ô thứ hai không hợp lệ. Vui lòng nhập số!",
                                "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSo2.Focus();
                return;
            }

            // Kiểm tra xem người dùng đã chọn phép tính chưa
            if (!radCong.Checked && !radTru.Checked && !radNhan.Checked && !radChia.Checked)
            {
                MessageBox.Show("Vui lòng chọn phép tính trước khi thực hiện!",
                                "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Thực hiện phép tính dựa vào phép toán được chọn
            if (radCong.Checked) kq = so1 + so2;
            else if (radTru.Checked) kq = so1 - so2;
            else if (radNhan.Checked) kq = so1 * so2;
            else if (radChia.Checked && so2 != 0) kq = so1 / so2;
            //Hiển thị kết quả lên trên ô kết quả
            txtKq.Text = kq.ToString();
        }
    }
}
