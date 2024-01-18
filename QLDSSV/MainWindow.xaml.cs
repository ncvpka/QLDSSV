using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace StudentManagement
{
    public partial class MainWindow : Window
    {
        // Tạo một danh sách để lưu trữ thông tin sinh viên
        private ObservableCollection<Student> students = new ObservableCollection<Student>();
        private Student studentBeingEdited;

        public MainWindow()
        {
            InitializeComponent();

            // Gán danh sách sinh viên cho DataGrid
            dataGridStudents.ItemsSource = students;
            studentBeingEdited = null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra nhập liệu
            if (string.IsNullOrEmpty(txtStudentID.Text) || string.IsNullOrEmpty(txtFullName.Text) || cmbGender.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sinh viên.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Thêm sinh viên mới vào danh sách
            students.Add(new Student
            {
                StudentID = txtStudentID.Text,
                FullName = txtFullName.Text,
                BirthDate = dpBirthDate.SelectedDate ?? DateTime.MinValue,
                Gender = (cmbGender.SelectedItem as ComboBoxItem)?.Content.ToString()
            });

            // Xóa thông tin đã nhập để chuẩn bị nhập sinh viên tiếp theo
            txtStudentID.Clear();
            txtFullName.Clear();
            dpBirthDate.SelectedDate = null;
            cmbGender.SelectedIndex = -1;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dòng được chọn
            var selectedStudent = ((FrameworkElement)sender).DataContext as Student;

            // Xóa sinh viên khỏi danh sách
            students.Remove(selectedStudent);
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dòng được chọn
            var selectedStudent = ((FrameworkElement)sender).DataContext as Student;

            // Hiển thị thông tin sinh viên trong các điều khiển nhập liệu để chỉnh sửa
            txtStudentID.Text = selectedStudent.StudentID;
            txtFullName.Text = selectedStudent.FullName;
            dpBirthDate.SelectedDate = selectedStudent.BirthDate;
            cmbGender.SelectedItem = selectedStudent.Gender;

            // Ẩn nút thêm, hiển thị nút cập nhật
            btnAdd.Visibility = Visibility.Collapsed;
            btnUpdate.Visibility = Visibility.Visible;

            // Lưu sinh viên đang chỉnh sửa để cập nhật sau khi nhấn nút cập nhật
            studentBeingEdited = selectedStudent;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra nhập liệu
            if (string.IsNullOrEmpty(txtStudentID.Text) || string.IsNullOrEmpty(txtFullName.Text) || cmbGender.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sinh viên.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Cập nhật thông tin sinh viên
            studentBeingEdited.StudentID = txtStudentID.Text;
            studentBeingEdited.FullName = txtFullName.Text;
            studentBeingEdited.BirthDate = dpBirthDate.SelectedDate ?? DateTime.MinValue;
            studentBeingEdited.Gender = cmbGender.SelectedItem.ToString();

            // Ẩn nút cập nhật, hiển thị nút thêm
            btnAdd.Visibility = Visibility.Visible;
            btnUpdate.Visibility = Visibility.Collapsed;

            // Xóa thông tin đã nhập để chuẩn bị nhập sinh viên tiếp theo
            txtStudentID.Clear();
            txtFullName.Clear();
            dpBirthDate.SelectedDate = null;
            cmbGender.SelectedIndex = -1;

            // Đặt lại biến studentBeingEdited về null
            studentBeingEdited = null;
        }


    }

    // Class đại diện cho thông tin sinh viên
    public class Student
    {
        public string StudentID { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
    }

}
