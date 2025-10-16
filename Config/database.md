# Database Structure - Gunny Game

## 📊 Tổng quan 4 Database

### 1. 🎮 **Db_Tank** - Database chính
**Chức năng:** Chứa tất cả dữ liệu game chính
- **User data** - Thông tin người chơi
- **Game items** - Vũ khí, trang bị, vật phẩm
- **Maps** - Bản đồ game
- **NPCs** - Nhân vật trong game
- **Quests** - Nhiệm vụ
- **Guilds** - Hội nhóm
- **Inventory** - Kho đồ
- **Character stats** - Chỉ số nhân vật
- **Game progress** - Tiến độ game

### 2. 📈 **Db_Count** - Database thống kê
**Chức năng:** Lưu trữ dữ liệu thống kê và phân tích
- **Player statistics** - Thống kê người chơi
- **Server logs** - Logs server
- **Performance data** - Dữ liệu hiệu suất
- **Game metrics** - Chỉ số game
- **User behavior** - Hành vi người dùng
- **Revenue tracking** - Theo dõi doanh thu
- **Analytics data** - Dữ liệu phân tích

### 3. 🔐 **Db_Membership** - Database thành viên
**Chức năng:** Quản lý hệ thống thành viên và xác thực
- **User accounts** - Tài khoản người dùng
- **User profiles** - Thông tin cá nhân
- **User roles** - Vai trò (Admin, User, VIP)
- **Authentication** - Xác thực đăng nhập
- **Session management** - Quản lý phiên đăng nhập
- **Password policies** - Chính sách mật khẩu
- **VIP levels** - Cấp độ VIP
- **Membership types** - Loại thành viên
- **Payment history** - Lịch sử thanh toán

### 4. 📝 **Db_Logs** - Database logs
**Chức năng:** Lưu trữ tất cả logs hệ thống
- **System logs** - Logs hệ thống
- **Error logs** - Logs lỗi
- **Access logs** - Logs truy cập
- **Game logs** - Logs game
- **Admin logs** - Logs admin
- **Security logs** - Logs bảo mật
- **Performance logs** - Logs hiệu suất
- **Audit trails** - Dấu vết kiểm tra

## 🔗 Mối quan hệ giữa các Database

```
Db_Membership (Authentication)
    ↓
Db_Tank (Game Data)
    ↓
Db_Count (Statistics)
    ↓
Db_Logs (All Logs)
```

## 🎯 Lợi ích của việc tách database

1. **Performance** - Tối ưu hiệu suất
2. **Security** - Bảo mật tốt hơn
3. **Maintenance** - Dễ bảo trì
4. **Scalability** - Dễ mở rộng
5. **Backup** - Backup riêng biệt
6. **Development** - Phát triển độc lập
