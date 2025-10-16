# Database Db_Membership - Phân tích chi tiết

## 📊 Tổng quan
- **Tổng số bảng:** 22 bảng
- **Tổng số người dùng:** 25,971 users
- **Chức năng:** Quản lý hệ thống thành viên, xác thực và thanh toán

## 🗂️ CẤU TRÚC BẢNG CHÍNH

### 1. 👤 **Mem_Users** (25,971 records)
**Bảng người dùng chính:**
- `UserID` - ID người dùng (Primary Key)
- `UserName` - Tên đăng nhập
- `Password` - Mật khẩu (đã mã hóa)
- `Email` - Email
- `IsAnonymous` - Tài khoản ẩn danh (bit)
- `LastActivityDate` - Lần hoạt động cuối (datetime)
- `LoginTime` - Thời gian đăng nhập (int)
- `Point` - Điểm số (int, default: 0)

### 2. 📋 **Mem_UserInfo** (25,971 records)
**Thông tin chi tiết người dùng:**
- `UserID` - ID người dùng (Foreign Key)
- `FailedPasswordAttemptCount` - Số lần nhập sai mật khẩu (int)
- `FailedPasswordAttemptWindowStart` - Thời gian bắt đầu nhập sai (datetime)
- `FailedPasswordAnswerAttemptCount` - Số lần nhập sai câu trả lời (int)
- `FailedPasswordAnswerAttemptWindowstart` - Thời gian bắt đầu nhập sai câu trả lời (datetime)
- `Comment` - Ghi chú (ntext)
- `UserSex` - Giới tính (bit)

### 3. 🛡️ **Mem_Roles** (12 records)
**Bảng vai trò:**
- `RoleID` - ID vai trò (Primary Key)
- `RoleName` - Tên vai trò
- `Description` - Mô tả vai trò

### 4. 🔐 **Mem_UserRight** (0 records)
**Quyền người dùng:**
- `Id` - ID quyền (Primary Key)
- `UserID` - ID người dùng (Foreign Key)
- `RightID` - ID quyền

### 5. 👥 **Mem_UsersInRoles** (0 records)
**Người dùng trong vai trò:**
- `UserID` - ID người dùng (Foreign Key)
- `RoleID` - ID vai trò (Foreign Key)

## 💳 BẢNG THANH TOÁN

### 6. 💳 **Pay_Card**
**Thẻ thanh toán:**
- Thông tin thẻ cào
- Mã thẻ, serial
- Trạng thái sử dụng

### 7. 🔄 **Pay_Exchange**
**Đổi thưởng:**
- Lịch sử đổi thưởng
- Điểm đổi, vật phẩm nhận

### 8. 📜 **Pay_History**
**Lịch sử thanh toán:**
- Giao dịch thanh toán
- Thời gian, số tiền
- Phương thức thanh toán

### 9. ℹ️ **Pay_Info**
**Thông tin thanh toán:**
- Chi tiết giao dịch
- Trạng thái thanh toán

### 10. 🎯 **Pay_Select**
**Lựa chọn thanh toán:**
- Các gói thanh toán
- Giá cả, ưu đãi

### 11. 🛒 **Pay_Way**
**Phương thức thanh toán:**
- Các cách thanh toán
- Cấu hình thanh toán

## 🔧 BẢNG HỆ THỐNG

### 12. 📱 **Mem_Application**
**Ứng dụng:**
- Thông tin ứng dụng
- Cấu hình app

### 13. 📱 **Mem_Application_Sub**
**Ứng dụng con:**
- Module con của app
- Phân quyền module

### 14. 🔑 **Mem_Code**
**Mã xác thực:**
- Mã kích hoạt
- Mã xác nhận

### 15. 🔑 **Mem_ActiveCode**
**Mã kích hoạt:**
- Mã kích hoạt tài khoản
- Trạng thái sử dụng

### 16. 🔄 **Mem_ResetPwd**
**Reset mật khẩu:**
- Mã reset password
- Thời gian hết hạn

### 17. 🧩 **Mem_Module**
**Module hệ thống:**
- Các module của hệ thống
- Phân quyền module

### 18. 🛤️ **Mem_Paths**
**Đường dẫn:**
- Các đường dẫn hệ thống
- Cấu hình path

### 19. ⚖️ **Mem_Right**
**Quyền hệ thống:**
- Danh sách quyền
- Mô tả quyền

### 20. 💾 **Mem_Users_Save**
**Backup người dùng:**
- Sao lưu thông tin user
- Lịch sử thay đổi

### 21. 🏪 **eStore**
**Cửa hàng điện tử:**
- Thông tin cửa hàng
- Sản phẩm, dịch vụ

### 22. 📋 **dtproperties**
**Thuộc tính hệ thống:**
- Metadata của database
- Cấu hình hệ thống

## 📈 THỐNG KÊ QUAN TRỌNG

- **25,971 người dùng** đã đăng ký
- **12 vai trò** khác nhau trong hệ thống
- **Hệ thống thanh toán** đầy đủ với 6 bảng
- **Bảo mật cao** với tracking failed attempts
- **Hệ thống phân quyền** chi tiết
- **Backup và recovery** được hỗ trợ

## 🔗 MỐI QUAN HỆ

```
Mem_Users (1) ←→ (1) Mem_UserInfo
Mem_Users (N) ←→ (N) Mem_Roles [qua Mem_UsersInRoles]
Mem_Users (1) ←→ (N) Pay_History
Mem_Users (1) ←→ (N) Mem_UserRight
```

## 🎯 CHỨC NĂNG CHÍNH

1. **Authentication** - Xác thực đăng nhập
2. **Authorization** - Phân quyền người dùng
3. **User Management** - Quản lý người dùng
4. **Payment System** - Hệ thống thanh toán
5. **Security** - Bảo mật và theo dõi
6. **Role Management** - Quản lý vai trò
