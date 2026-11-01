function switchTab(tab) {
    document.querySelectorAll('.tab-content').forEach(el => el.classList.remove('active'));
    document.querySelectorAll('.nav-link').forEach(el => el.classList.remove('active'));

    if (tab === 'dang-ky') {
        document.getElementById('tab-dang-ky').classList.add('active');
        document.getElementById('nav-dang-ky').classList.add('active');
        document.getElementById('page-heading').textContent = 'Đăng ký bệnh nhân mới';
    } else {
        document.getElementById('tab-kham-benh').classList.add('active');
        document.getElementById('nav-kham-benh').classList.add('active');
        document.getElementById('page-heading').textContent = 'Khám bệnh & Kê toa thuốc';
    }
}

function updateFileName(input) {
    const display = document.getElementById('fileNameDisplay');
    if (input.files && input.files.length > 0) {
        display.textContent = "Đã chọn: " + input.files[0].name;
    } else {
        display.textContent = "Tải ảnh 3x4 lên hệ thống";
    }
}

function resetForm() {
    document.getElementById('fileNameDisplay').textContent = "Tải ảnh 3x4 lên hệ thống";
}

function addMedRow() {
    const tbody = document.getElementById('medTableBody');
    const tr = document.createElement('tr');
    tr.innerHTML = `
        <td>
            <select class="form-control" required>
                <option value="1">Paracetamol 500mg (Viên)</option>
                <option value="2">Vitamin C 500mg (Viên)</option>
                <option value="3">Oresol (Gói)</option>
            </select>
        </td>
        <td><input type="number" class="form-control" min="1" value="1" required></td>
        <td><input type="number" class="form-control" min="1" value="1" required></td>
        <td><input type="text" class="form-control" placeholder="Liều dùng & cách dùng" required></td>
        <td style="text-align: center;"><button type="button" class="btn-danger-sm" onclick="removeRow(this)"><i class="fa-solid fa-trash"></i></button></td>
    `;
    tbody.appendChild(tr);
}

function removeRow(btn) {
    btn.closest('tr').remove();
}

document.addEventListener('DOMContentLoaded', function() {
    const formPatient = document.getElementById('formPatient');
    if(formPatient) {
        formPatient.addEventListener('submit', function(e) {
            e.preventDefault();
            alert('Lưu hồ sơ bệnh nhân thành công!');
        });
    }

    const formPrescription = document.getElementById('formPrescription');
    if(formPrescription) {
        formPrescription.addEventListener('submit', function(e) {
            e.preventDefault();
            alert('Đã lưu lượt khám và đơn thuốc thành công!');
        });
    }
});