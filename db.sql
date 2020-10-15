/*
 Navicat Premium Data Transfer

 Source Server         : data
 Source Server Type    : SQL Server
 Source Server Version : 13004001
 Source Host           : (localdb)\MSSQLLocalDB:1433
 Source Catalog        : datareporter
 Source Schema         : dbo

 Target Server Type    : SQL Server
 Target Server Version : 13004001
 File Encoding         : 65001

 Date: 14/10/2020 11:08:42
*/


-- ----------------------------
-- Table structure for DR_account
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_account]') AND type IN ('U'))
	DROP TABLE [dbo].[DR_account]
GO

CREATE TABLE [dbo].[DR_account] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [account_no] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [account_name] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [rg_code] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [type_code] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [type_name] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [AgencyNo] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[DR_account] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of DR_account
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[DR_account] ON
GO

INSERT INTO [dbo].[DR_account] ([Id], [account_no], [account_name], [rg_code], [type_code], [type_name], [AgencyNo]) VALUES (N'1', N'210130203010013', N'广南县财政局国库股', N'532627', N'01', N'非税收入专户', N'84302'), (N'79', N'8710214020000000050', N'嵩明县财政局', N'530127', N'01', N'非税收入专户', N'84302')
GO

SET IDENTITY_INSERT [dbo].[DR_account] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for DR_admin
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_admin]') AND type IN ('U'))
	DROP TABLE [dbo].[DR_admin]
GO

CREATE TABLE [dbo].[DR_admin] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [No.] nvarchar(6) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [UserName] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [password] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[DR_admin] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for DR_AgencyInformation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_AgencyInformation]') AND type IN ('U'))
	DROP TABLE [dbo].[DR_AgencyInformation]
GO

CREATE TABLE [dbo].[DR_AgencyInformation] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [AgencyNo] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [account_bank] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [account_bank_code] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [banktype_code] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [banktype_name] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL
)
GO

ALTER TABLE [dbo].[DR_AgencyInformation] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of DR_AgencyInformation
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[DR_AgencyInformation] ON
GO

INSERT INTO [dbo].[DR_AgencyInformation] ([Id], [AgencyNo], [account_bank], [account_bank_code], [banktype_code], [banktype_name]) VALUES (N'1', N'84302', N'文山广南长江村镇银行', N'320745710016', N'320', N'村镇银行'), (N'2', N'84301', N'1', N'1', N'1', N'1')
GO

SET IDENTITY_INSERT [dbo].[DR_AgencyInformation] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for DR_data
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_data]') AND type IN ('U'))
	DROP TABLE [dbo].[DR_data]
GO

CREATE TABLE [dbo].[DR_data] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [inputTime] datetime  NULL,
  [data_position] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [AgencyNo] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [dataName] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [datatype] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [upload] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[DR_data] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for DR_FTPInformation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_FTPInformation]') AND type IN ('U'))
	DROP TABLE [dbo].[DR_FTPInformation]
GO

CREATE TABLE [dbo].[DR_FTPInformation] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [FTPAddress] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [FTPUsername] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [FTPPassword] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [data_Key] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [AgencyNo] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[DR_FTPInformation] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of DR_FTPInformation
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[DR_FTPInformation] ON
GO

INSERT INTO [dbo].[DR_FTPInformation] ([Id], [FTPAddress], [FTPUsername], [FTPPassword], [data_Key], [AgencyNo]) VALUES (N'1', N'192.168.0.103', N'ftpuser', N'1', N'A23C809Z60R9CR9RZ1LQVQCC', N'84302')
GO

SET IDENTITY_INSERT [dbo].[DR_FTPInformation] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for DR_import_logs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_import_logs]') AND type IN ('U'))
	DROP TABLE [dbo].[DR_import_logs]
GO

CREATE TABLE [dbo].[DR_import_logs] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [log_No] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [inputtime] datetime  NULL,
  [AgencyNo] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [logName] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [succeed] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[DR_import_logs] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of DR_import_logs
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[DR_import_logs] ON
GO

INSERT INTO [dbo].[DR_import_logs] ([Id], [log_No], [inputtime], [AgencyNo], [logName], [succeed]) VALUES (N'1', N'20201004013845', N'2020-10-04 01:38:45.973', N'84302', N'生成流水明文：32011120201005', N'1'), (N'2', N'20201004014103', N'2020-10-04 01:41:03.957', N'84302', N'生成流水明文：32011120201005', N'1'), (N'3', N'20201004014208', N'2020-10-04 01:42:08.620', N'84302', N'生成流水明文：32011120201005', N'1'), (N'4', N'20201004105421', N'2020-10-04 22:54:20.090', N'84302', N'生成流水明文：320111202010050004', N'1'), (N'5', N'20201004105533', N'2020-10-04 22:55:13.600', N'84302', N'生成流水密文：320111202010050004', N'1'), (N'6', N'20201004110103', N'2020-10-04 23:01:03.563', N'84302', N'生成流水明文：320111202010050005', N'1'), (N'7', N'20201004110106', N'2020-10-04 23:01:06.097', N'84302', N'生成流水密文：320111202010050005', N'1'), (N'8', N'20201004110243', N'2020-10-04 23:02:43.347', N'84302', N'生成流水明文：320111202010050006', N'1'), (N'9', N'20201004110245', N'2020-10-04 23:02:45.263', N'84302', N'生成流水密文：320111202010050006', N'1'), (N'10', N'20201004110353', N'2020-10-04 23:03:53.723', N'84302', N'生成流水明文：320111202010050007', N'1'), (N'11', N'20201004110356', N'2020-10-04 23:03:56.903', N'84302', N'生成流水密文：320111202010050007', N'1'), (N'12', N'20201004110527', N'2020-10-04 23:05:27.220', N'84302', N'生成流水明文：320111202010050008', N'1'), (N'13', N'20201004110528', N'2020-10-04 23:05:28.330', N'84302', N'生成流水密文：320111202010050008', N'1'), (N'14', N'20201004110749', N'2020-10-04 23:07:49.210', N'84302', N'生成流水明文：320111202010050009', N'1'), (N'15', N'20201004110749', N'2020-10-04 23:07:49.273', N'84302', N'生成流水密文：320111202010050009', N'1'), (N'16', N'20201004111130', N'2020-10-04 23:11:30.513', N'84302', N'生成流水明文：320111202010050010', N'1'), (N'17', N'20201004111132', N'2020-10-04 23:11:32.267', N'84302', N'生成流水密文：320111202010050010', N'1'), (N'18', N'20201004111524', N'2020-10-04 23:15:24.483', N'84302', N'生成流水明文：320111202010050011', N'1'), (N'19', N'20201004111524', N'2020-10-04 23:15:24.557', N'84302', N'生成流水密文：320111202010050011', N'1'), (N'20', N'20201004111804', N'2020-10-04 23:18:04.187', N'84302', N'生成流水明文：320111202010050012', N'1'), (N'21', N'20201004111806', N'2020-10-04 23:18:06.317', N'84302', N'生成流水密文：320111202010050012', N'1'), (N'22', N'20201004112046', N'2020-10-04 23:20:46.043', N'84302', N'生成流水明文：320111202010050013', N'1'), (N'23', N'20201004112049', N'2020-10-04 23:20:49.060', N'84302', N'生成流水密文：320111202010050013', N'1'), (N'24', N'20201004112129', N'2020-10-04 23:21:28.187', N'84302', N'上传流水密文：320111202010050013不成功', N'0'), (N'25', N'20201004112210', N'2020-10-04 23:22:08.657', N'84302', N'生成余额明文：320011202010050001', N'1'), (N'26', N'20201004112233', N'2020-10-04 23:22:32.237', N'84302', N'生成余额密文：320011202010050001', N'1'), (N'27', N'20201004112453', N'2020-10-04 23:24:53.743', N'84302', N'生成流水明文：320111202010050014', N'1'), (N'28', N'20201004112454', N'2020-10-04 23:24:54.020', N'84302', N'生成流水密文：320111202010050014', N'1'), (N'29', N'20201004112519', N'2020-10-04 23:25:19.713', N'84302', N'上传流水密文：320111202010050014不成功', N'0'), (N'30', N'20201004112520', N'2020-10-04 23:25:20.690', N'84302', N'生成余额明文：320011202010050003', N'1'), (N'31', N'20201004112520', N'2020-10-04 23:25:20.717', N'84302', N'生成余额密文：320011202010050003', N'1'), (N'32', N'20201004112541', N'2020-10-04 23:25:41.743', N'84302', N'上传余额密文：320011202010050003不成功', N'1'), (N'33', N'20201005084647', N'2020-10-05 08:46:47.973', N'84302', N'生成流水明文：320101532627202010060001', N'1'), (N'34', N'20201005084650', N'2020-10-05 08:46:50.050', N'84302', N'生成流水密文：320101532627202010060001', N'1'), (N'35', N'20201005084711', N'2020-10-05 08:47:11.287', N'84302', N'上传流水密文：320101532627202010060001不成功', N'0'), (N'36', N'20201005084711', N'2020-10-05 08:47:11.327', N'84302', N'生成余额明文：320001532627202010060001', N'1'), (N'37', N'20201005084711', N'2020-10-05 08:47:11.363', N'84302', N'生成余额密文：320001532627202010060001', N'1'), (N'38', N'20201005084732', N'2020-10-05 08:47:32.403', N'84302', N'上传余额密文：320001532627202010060001不成功', N'1'), (N'39', N'20201005084958', N'2020-10-05 08:49:58.823', N'84302', N'生成流水明文：320101532627202010060002', N'1'), (N'40', N'20201005084958', N'2020-10-05 08:49:58.883', N'84302', N'生成流水密文：320101532627202010060002', N'1'), (N'41', N'20201005085020', N'2020-10-05 08:50:20.967', N'84302', N'上传流水密文：320101532627202010060002不成功', N'0'), (N'42', N'20201005085021', N'2020-10-05 08:50:21.007', N'84302', N'生成余额明文：320001532627202010060003', N'1'), (N'43', N'20201005085021', N'2020-10-05 08:50:21.033', N'84302', N'生成余额密文：320001532627202010060003', N'1'), (N'44', N'20201005085042', N'2020-10-05 08:50:42.240', N'84302', N'上传余额密文：320001532627202010060003不成功', N'1'), (N'45', N'20201005085327', N'2020-10-05 08:53:27.517', N'84302', N'生成流水明文：320101532627202010060003', N'1'), (N'46', N'20201005085327', N'2020-10-05 08:53:27.933', N'84302', N'生成流水密文：320101532627202010060003', N'1'), (N'47', N'20201005085349', N'2020-10-05 08:53:49.657', N'84302', N'上传流水密文：320101532627202010060003不成功', N'0'), (N'48', N'20201005085349', N'2020-10-05 08:53:49.687', N'84302', N'生成余额明文：320001532627202010060005', N'1'), (N'49', N'20201005085349', N'2020-10-05 08:53:49.717', N'84302', N'生成余额密文：320001532627202010060005', N'1'), (N'50', N'20201005085410', N'2020-10-05 08:54:10.877', N'84302', N'上传余额密文：320001532627202010060005不成功', N'1'), (N'51', N'20201005090055', N'2020-10-05 09:00:55.200', N'84302', N'生成流水明文：320111202010060001', N'1'), (N'52', N'20201005090057', N'2020-10-05 09:00:57.663', N'84302', N'生成流水密文：320111202010060001', N'1'), (N'53', N'20201005090119', N'2020-10-05 09:01:19.680', N'84302', N'上传流水密文：320111202010060001不成功', N'0'), (N'54', N'20201005090119', N'2020-10-05 09:01:19.740', N'84302', N'生成余额明文：320011202010060001', N'1'), (N'55', N'20201005090119', N'2020-10-05 09:01:19.927', N'84302', N'生成余额密文：320011202010060001', N'1'), (N'56', N'20201005090141', N'2020-10-05 09:01:41.077', N'84302', N'上传余额密文：320011202010060001不成功', N'1'), (N'57', N'20201005090513', N'2020-10-05 09:05:13.967', N'84302', N'生成流水明文：320101532627202010060004', N'1'), (N'58', N'20201005090514', N'2020-10-05 09:05:14.350', N'84302', N'生成流水密文：320101532627202010060004', N'1'), (N'59', N'20201005090535', N'2020-10-05 09:05:35.593', N'84302', N'上传流水密文：320101532627202010060004不成功', N'0'), (N'60', N'20201005090535', N'2020-10-05 09:05:35.600', N'84302', N'生成余额明文：320001532627202010060007', N'1'), (N'61', N'20201005090535', N'2020-10-05 09:05:35.620', N'84302', N'生成余额密文：320001532627202010060007', N'1'), (N'62', N'20201005090556', N'2020-10-05 09:05:56.650', N'84302', N'上传余额密文：320001532627202010060007不成功', N'1'), (N'63', N'20201005093310', N'2020-10-05 09:33:10.260', N'84302', N'生成余额明文：320001532627202010060009', N'1'), (N'64', N'20201005093311', N'2020-10-05 09:33:11.587', N'84302', N'生成余额密文：320001532627202010060009', N'1'), (N'65', N'20201005093322', N'2020-10-05 09:33:22.690', N'84302', N'生成余额明文：320001532627202010060011', N'1'), (N'66', N'20201005093322', N'2020-10-05 09:33:22.893', N'84302', N'生成余额密文：320001532627202010060011', N'1'), (N'67', N'20201005094045', N'2020-10-05 09:40:45.500', N'84302', N'生成余额明文：320001532627202010060013', N'1'), (N'68', N'20201005094046', N'2020-10-05 09:40:46.887', N'84302', N'生成余额密文：320001532627202010060013', N'1'), (N'69', N'20201005094108', N'2020-10-05 09:41:08.633', N'84302', N'上传余额密文：320001532627202010060013不成功', N'1'), (N'70', N'20201005094138', N'2020-10-05 09:41:38.133', N'84302', N'生成余额明文：320001532627202010060015', N'1'), (N'71', N'20201005094138', N'2020-10-05 09:41:38.173', N'84302', N'生成余额密文：320001532627202010060015', N'1'), (N'72', N'20201005094159', N'2020-10-05 09:41:59.397', N'84302', N'上传余额密文：320001532627202010060015不成功', N'1'), (N'73', N'20201005094807', N'2020-10-05 09:48:07.347', N'84302', N'生成余额明文：320001532627202010060017', N'1'), (N'74', N'20201005094807', N'2020-10-05 09:48:07.487', N'84302', N'生成余额密文：320001532627202010060017', N'1'), (N'75', N'20201005105250', N'2020-10-05 10:52:50.123', N'84302', N'生成余额明文：320001532627202010060019', N'1'), (N'76', N'20201005105251', N'2020-10-05 10:52:51.497', N'84302', N'生成余额密文：320001532627202010060019', N'1'), (N'77', N'20201005105312', N'2020-10-05 10:53:12.770', N'84302', N'上传余额密文：320001532627202010060019不成功', N'1'), (N'78', N'20201005083541', N'2020-10-05 20:35:41.943', N'84302', N'生成流水明文：320101532627202010060005', N'1'), (N'79', N'20201005083543', N'2020-10-05 20:35:43.457', N'84302', N'生成流水密文：320101532627202010060005', N'1'), (N'80', N'20201005083604', N'2020-10-05 20:36:04.880', N'84302', N'上传流水密文：320101532627202010060005不成功', N'0'), (N'81', N'20201005083604', N'2020-10-05 20:36:04.920', N'84302', N'生成余额明文：320001532627202010060021', N'1'), (N'82', N'20201005083604', N'2020-10-05 20:36:04.950', N'84302', N'生成余额密文：320001532627202010060021', N'1'), (N'83', N'20201005083625', N'2020-10-05 20:36:25.980', N'84302', N'上传余额密文：320001532627202010060021不成功', N'1'), (N'84', N'20201005092143', N'2020-10-05 21:21:43.737', N'84302', N'生成流水明文：320101532627202010060001', N'1'), (N'85', N'20201005092144', N'2020-10-05 21:21:44.373', N'84302', N'生成余额明文：320001532627202010060001', N'1'), (N'86', N'20201005092346', N'2020-10-05 21:23:45.727', N'84302', N'生成流水明文：320101532627202010060001', N'1'), (N'87', N'20201005092558', N'2020-10-05 21:25:58.303', N'84302', N'生成余额明文：320001532627202010060001', N'1'), (N'88', N'20201005092701', N'2020-10-05 21:27:01.910', N'84302', N'生成流水明文：320101532627202010060001', N'1'), (N'89', N'20201005092703', N'2020-10-05 21:27:03.083', N'84302', N'生成流水密文：320101532627202010060001', N'1'), (N'90', N'20201005092724', N'2020-10-05 21:27:24.187', N'84302', N'上传流水密文：320101532627202010060001不成功', N'0'), (N'91', N'20201005092724', N'2020-10-05 21:27:24.193', N'84302', N'生成余额明文：320001532627202010060001', N'1'), (N'92', N'20201005092724', N'2020-10-05 21:27:24.217', N'84302', N'生成余额密文：320001532627202010060001', N'1'), (N'93', N'20201005092745', N'2020-10-05 21:27:45.240', N'84302', N'上传余额密文：320001532627202010060001不成功', N'1'), (N'94', N'20201005092822', N'2020-10-05 21:28:22.197', N'84302', N'生成余额明文：320001532627202010060003', N'1'), (N'95', N'20201005092826', N'2020-10-05 21:28:26.153', N'84302', N'生成余额明文：320001532627202010060005', N'1'), (N'96', N'20201005092826', N'2020-10-05 21:28:26.187', N'84302', N'生成余额密文：320001532627202010060005', N'1'), (N'97', N'20201005092847', N'2020-10-05 21:28:47.237', N'84302', N'上传余额密文：320001532627202010060005不成功', N'1'), (N'98', N'20201005095852', N'2020-10-05 21:58:52.850', N'84302', N'生成流水明文：320101532627202010060002', N'1'), (N'99', N'20201005095853', N'2020-10-05 21:58:53.413', N'84302', N'生成流水密文：320101532627202010060002', N'1'), (N'100', N'20201005095915', N'2020-10-05 21:59:15.013', N'84302', N'上传流水密文：320101532627202010060002不成功', N'0'), (N'101', N'20201005095915', N'2020-10-05 21:59:15.077', N'84302', N'生成余额明文：320001532627202010060007', N'1'), (N'102', N'20201005095915', N'2020-10-05 21:59:15.317', N'84302', N'生成余额密文：320001532627202010060007', N'1'), (N'103', N'20201005095936', N'2020-10-05 21:59:36.673', N'84302', N'上传余额密文：320001532627202010060007不成功', N'1'), (N'104', N'20201005095952', N'2020-10-05 21:59:52.693', N'84302', N'生成流水明文：320101532627202010060003', N'1'), (N'105', N'20201005095952', N'2020-10-05 21:59:52.823', N'84302', N'生成流水密文：320101532627202010060003', N'1'), (N'106', N'20201005100014', N'2020-10-05 22:00:14.110', N'84302', N'上传流水密文：320101532627202010060003不成功', N'0'), (N'107', N'20201005100014', N'2020-10-05 22:00:14.197', N'84302', N'生成余额明文：320001532627202010060009', N'1'), (N'108', N'20201005100014', N'2020-10-05 22:00:14.237', N'84302', N'生成余额密文：320001532627202010060009', N'1'), (N'109', N'20201005100035', N'2020-10-05 22:00:35.277', N'84302', N'上传余额密文：320001532627202010060009不成功', N'1'), (N'110', N'20201005100336', N'2020-10-05 22:03:36.227', N'84302', N'生成流水明文：320101532627202010060004', N'1'), (N'111', N'20201005100336', N'2020-10-05 22:03:36.600', N'84302', N'生成流水密文：320101532627202010060004', N'1'), (N'112', N'20201005100357', N'2020-10-05 22:03:57.977', N'84302', N'上传流水密文：320101532627202010060004不成功', N'0'), (N'113', N'20201005100357', N'2020-10-05 22:03:57.987', N'84302', N'生成余额明文：320001532627202010060011', N'1'), (N'114', N'20201005100358', N'2020-10-05 22:03:58.037', N'84302', N'生成余额密文：320001532627202010060011', N'1'), (N'115', N'20201005100419', N'2020-10-05 22:04:19.130', N'84302', N'上传余额密文：320001532627202010060011不成功', N'1'), (N'116', N'20201005100703', N'2020-10-05 22:07:03.940', N'84302', N'生成余额明文：320001532627202010060013', N'1'), (N'117', N'20201005100704', N'2020-10-05 22:07:04.100', N'84302', N'生成余额密文：320001532627202010060013', N'1'), (N'118', N'20201005100725', N'2020-10-05 22:07:25.303', N'84302', N'上传余额密文：320001532627202010060013不成功', N'1'), (N'119', N'20201005110449', N'2020-10-05 23:04:49.857', N'84302', N'生成流水明文：320101532627202010060005', N'1'), (N'120', N'20201005110451', N'2020-10-05 23:04:51.520', N'84302', N'生成流水密文：320101532627202010060005', N'1'), (N'121', N'20201005110512', N'2020-10-05 23:05:12.910', N'84302', N'上传流水密文：320101532627202010060005不成功', N'0'), (N'122', N'20201005110512', N'2020-10-05 23:05:12.980', N'84302', N'生成余额明文：320001532627202010060002', N'1'), (N'123', N'20201005110513', N'2020-10-05 23:05:13.333', N'84302', N'生成余额密文：320001532627202010060002', N'1'), (N'124', N'20201005110816', N'2020-10-05 23:08:16.980', N'84302', N'生成流水明文：320101532627202010060006', N'1'), (N'125', N'20201005110818', N'2020-10-05 23:08:18.397', N'84302', N'生成流水密文：320101532627202010060006', N'1'), (N'126', N'20201005110839', N'2020-10-05 23:08:39.960', N'84302', N'上传流水密文：320101532627202010060006不成功', N'0'), (N'127', N'20201005110839', N'2020-10-05 23:08:39.990', N'84302', N'生成余额明文：320001532627202010060004', N'1'), (N'128', N'20201005110840', N'2020-10-05 23:08:40.197', N'84302', N'生成余额密文：320001532627202010060004', N'1'), (N'129', N'20201005110901', N'2020-10-05 23:09:01.533', N'84302', N'上传余额密文：320001532627202010060004不成功', N'1'), (N'130', N'20201005110920', N'2020-10-05 23:09:20.007', N'84302', N'生成余额明文：320001532627202010060006', N'1'), (N'131', N'20201005110920', N'2020-10-05 23:09:20.080', N'84302', N'生成余额密文：320001532627202010060006', N'1'), (N'132', N'20201005110941', N'2020-10-05 23:09:41.297', N'84302', N'上传余额密文：320001532627202010060006不成功', N'1'), (N'133', N'20201011120027', N'2020-10-11 00:00:27.287', N'84302', N'生成流水明文：320123523234234202010110001', N'1'), (N'134', N'20201011120029', N'2020-10-11 00:00:29.947', N'84302', N'生成流水密文：320123523234234202010110001', N'1'), (N'135', N'20201011120052', N'2020-10-11 00:00:52.333', N'84302', N'上传流水密文：320123523234234202010110001不成功', N'0'), (N'136', N'20201011120052', N'2020-10-11 00:00:52.403', N'84302', N'生成余额明文：320023523234234202010110001', N'1'), (N'137', N'20201011120052', N'2020-10-11 00:00:52.457', N'84302', N'生成余额密文：320023523234234202010110001', N'1'), (N'138', N'20201011120113', N'2020-10-11 00:01:13.800', N'84302', N'上传余额密文：320023523234234202010110001不成功', N'1'), (N'139', N'20201012020101', N'2020-10-12 14:01:01.160', N'84302', N'上传密文：320111202010050003', N'0'), (N'140', N'20201012020240', N'2020-10-12 14:02:40.503', N'84302', N'上传密文：320011202010050001', N'0'), (N'141', N'20201012020248', N'2020-10-12 14:02:48.983', N'84302', N'上传密文：320023523234234202010110001', N'0'), (N'142', N'20201012020303', N'2020-10-12 14:03:03.917', N'84302', N'上传密文：320023523234234202010110001', N'0'), (N'143', N'20201012020357', N'2020-10-12 14:03:57.970', N'84302', N'上传密文：320023523234234202010110001', N'0'), (N'144', N'20201012020422', N'2020-10-12 14:04:22.743', N'84302', N'上传密文：320123523234234202010110001', N'0'), (N'145', N'20201012020536', N'2020-10-12 14:05:36.143', N'84302', N'生成流水明文：320111202010130001', N'1'), (N'146', N'20201012020537', N'2020-10-12 14:05:37.037', N'84302', N'生成流水密文：320111202010130001', N'1'), (N'147', N'20201012020545', N'2020-10-12 14:05:45.280', N'84302', N'上传流水密文：320111202010130001不成功', N'0'), (N'148', N'20201012020545', N'2020-10-12 14:05:45.307', N'84302', N'生成余额明文：320011202010130001', N'1'), (N'149', N'20201012020545', N'2020-10-12 14:05:45.343', N'84302', N'生成余额密文：320011202010130001', N'1'), (N'150', N'20201012020557', N'2020-10-12 14:05:57.540', N'84302', N'上传余额密文：320011202010130001不成功', N'1'), (N'151', N'20201012021116', N'2020-10-12 14:11:16.823', N'84302', N'生成流水明文：320111202010130002', N'1'), (N'152', N'20201012021116', N'2020-10-12 14:11:16.920', N'84302', N'生成流水密文：320111202010130002', N'1'), (N'153', N'20201012021137', N'2020-10-12 14:11:37.940', N'84302', N'上传流水密文：320111202010130002不成功', N'0'), (N'154', N'20201012021137', N'2020-10-12 14:11:37.947', N'84302', N'生成余额明文：320011202010130002', N'1'), (N'155', N'20201012021137', N'2020-10-12 14:11:37.970', N'84302', N'生成余额密文：320011202010130002', N'1'), (N'156', N'20201012021158', N'2020-10-12 14:11:58.977', N'84302', N'上传余额密文：320011202010130002不成功', N'1'), (N'157', N'20201012021259', N'2020-10-12 14:12:59.210', N'84302', N'上传密文：320011202010130002', N'0'), (N'158', N'20201012021315', N'2020-10-12 14:13:15.887', N'84302', N'上传密文：320011202010130002', N'0'), (N'159', N'20201012021408', N'2020-10-12 14:14:08.323', N'84302', N'上传密文：320011202010130002', N'0'), (N'160', N'20201012021418', N'2020-10-12 14:14:18.997', N'84302', N'上传密文：320011202010130002', N'0'), (N'161', N'20201012021535', N'2020-10-12 14:15:35.957', N'84302', N'上传密文：320011202010130002', N'0'), (N'162', N'20201012023506', N'2020-10-12 14:35:06.070', N'84302', N'上传密文：320011202010130002', N'0'), (N'163', N'20201012023517', N'2020-10-12 14:35:17.023', N'84302', N'上传密文：320011202010130002', N'0'), (N'164', N'20201012023542', N'2020-10-12 14:35:42.583', N'84302', N'上传密文：320011202010130002', N'0'), (N'165', N'20201012023650', N'2020-10-12 14:36:50.137', N'84302', N'上传密文：320011202010130002', N'0'), (N'166', N'20201012023732', N'2020-10-12 14:37:32.777', N'84302', N'上传密文：320011202010130002', N'0'), (N'167', N'20201012023758', N'2020-10-12 14:37:58.010', N'84302', N'上传密文：320011202010130002', N'0'), (N'168', N'20201012024235', N'2020-10-12 14:42:35.373', N'84302', N'上传密文：320011202010130002', N'0'), (N'169', N'20201012025103', N'2020-10-12 14:51:03.707', N'84302', N'上传密文：320011202010130002', N'0'), (N'170', N'20201012030023', N'2020-10-12 15:00:23.750', N'84302', N'上传密文：320011202010130002', N'0'), (N'171', N'20201012030610', N'2020-10-12 15:06:10.897', N'84302', N'上传密文：320011202010130002', N'0'), (N'172', N'20201012031014', N'2020-10-12 15:10:14.350', N'84302', N'上传密文：320011202010130002', N'0'), (N'173', N'20201012032500', N'2020-10-12 15:25:00.060', N'84302', N'上传密文：320011202010130002', N'0'), (N'174', N'20201012034442', N'2020-10-12 15:44:42.463', N'84302', N'上传密文：320011202010130002', N'0'), (N'175', N'20201012041316', N'2020-10-12 16:13:16.123', N'84302', N'上传密文：320011202010130002', N'0'), (N'176', N'20201012041434', N'2020-10-12 16:14:34.597', N'84302', N'上传密文：320011202010130002', N'0'), (N'177', N'20201012041440', N'2020-10-12 16:14:40.897', N'84302', N'上传密文：320011202010130002', N'0'), (N'178', N'20201012041817', N'2020-10-12 16:18:17.990', N'84302', N'上传密文：320011202010130002', N'0'), (N'179', N'20201012042752', N'2020-10-12 16:27:52.123', N'84302', N'上传密文：320011202010130002', N'0'), (N'180', N'20201012043727', N'2020-10-12 16:37:27.503', N'84302', N'上传密文：320011202010130002', N'0'), (N'181', N'20201012044445', N'2020-10-12 16:44:45.817', N'84302', N'上传密文：320011202010130002', N'0'), (N'182', N'20201012060032', N'2020-10-12 18:00:32.230', N'84302', N'生成流水明文：320111202010130003', N'1'), (N'183', N'20201012060034', N'2020-10-12 18:00:34.453', N'84302', N'生成流水密文：320111202010130003', N'1'), (N'184', N'20201012060034', N'2020-10-12 18:00:34.547', N'84302', N'上传流水密文：320111202010130003不成功', N'0'), (N'185', N'20201012060034', N'2020-10-12 18:00:34.587', N'84302', N'生成余额明文：320011202010130003', N'1'), (N'186', N'20201012060034', N'2020-10-12 18:00:34.627', N'84302', N'生成余额密文：320011202010130003', N'1'), (N'187', N'20201012060034', N'2020-10-12 18:00:34.630', N'84302', N'上传余额密文：320011202010130003不成功', N'1'), (N'188', N'20201012060214', N'2020-10-12 18:02:14.540', N'84302', N'生成流水明文：320111202010130004', N'1'), (N'189', N'20201012060214', N'2020-10-12 18:02:14.610', N'84302', N'生成流水密文：320111202010130004', N'1'), (N'190', N'20201012060214', N'2020-10-12 18:02:14.680', N'84302', N'上传流水密文：320111202010130004不成功', N'0'), (N'191', N'20201012060214', N'2020-10-12 18:02:14.690', N'84302', N'生成余额明文：320011202010130004', N'1'), (N'192', N'20201012060214', N'2020-10-12 18:02:14.717', N'84302', N'生成余额密文：320011202010130004', N'1'), (N'193', N'20201012060214', N'2020-10-12 18:02:14.723', N'84302', N'上传余额密文：320011202010130004不成功', N'1'), (N'194', N'20201012060414', N'2020-10-12 18:04:14.310', N'84302', N'生成流水明文：320111202010130005', N'1'), (N'195', N'20201012060417', N'2020-10-12 18:04:17.553', N'84302', N'生成流水密文：320111202010130005', N'1'), (N'196', N'20201012060418', N'2020-10-12 18:04:18.253', N'84302', N'上传流水密文：320111202010130005不成功', N'0'), (N'197', N'20201012060418', N'2020-10-12 18:04:18.323', N'84302', N'生成余额明文：320011202010130005', N'1'), (N'198', N'20201012060418', N'2020-10-12 18:04:18.357', N'84302', N'生成余额密文：320011202010130005', N'1'), (N'199', N'20201012060418', N'2020-10-12 18:04:18.630', N'84302', N'上传余额密文：320011202010130005不成功', N'1')
GO

SET IDENTITY_INSERT [dbo].[DR_import_logs] OFF
GO

COMMIT
GO


-- ----------------------------
-- Table structure for DR_logs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_logs]') AND type IN ('U'))
	DROP TABLE [dbo].[DR_logs]
GO

CREATE TABLE [dbo].[DR_logs] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [log_No] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [inputtime] datetime  NULL,
  [AgencyNo] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [logName] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [succeed] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[DR_logs] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Table structure for DR_UserInformation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_UserInformation]') AND type IN ('U'))
	DROP TABLE [dbo].[DR_UserInformation]
GO

CREATE TABLE [dbo].[DR_UserInformation] (
  [Id] int  IDENTITY(1,1) NOT NULL,
  [username] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NOT NULL,
  [userpassword] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL,
  [AgencyNo] nvarchar(50) COLLATE SQL_Latin1_General_CP1_CI_AS  NULL
)
GO

ALTER TABLE [dbo].[DR_UserInformation] SET (LOCK_ESCALATION = TABLE)
GO


-- ----------------------------
-- Records of DR_UserInformation
-- ----------------------------
BEGIN TRANSACTION
GO

SET IDENTITY_INSERT [dbo].[DR_UserInformation] ON
GO

INSERT INTO [dbo].[DR_UserInformation] ([Id], [username], [userpassword], [AgencyNo]) VALUES (N'1', N'user', N'3B73CCA8B7D9D93A834631FB22769334', N'84302'), (N'2', N'admin', N'3B73CCA8B7D9D93A834631FB22769334', N'84302')
GO

SET IDENTITY_INSERT [dbo].[DR_UserInformation] OFF
GO

COMMIT
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromdataByID
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromdataByID]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromdataByID]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromdataByID]
        @ID [int]        
AS
BEGIN
        Select * from DR_data where Id=@ID
END
GO


-- ----------------------------
-- Procedure structure for DR_Updateaccount
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_Updateaccount]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_Updateaccount]
GO

CREATE PROCEDURE [dbo].[DR_Updateaccount]
        @ID int,
@account_no nvarchar(50),
@account_name nvarchar(50),
@rg_code nvarchar(50),
@type_code nvarchar(50),
@type_name nvarchar(50),
        @AgencyNo nvarchar(50)
AS
BEGIN
        Update DR_account
        SET account_no=@account_no,
account_name=@account_name,
rg_code=@rg_code,
type_code=@type_code,
type_name=@type_name,
            AgencyNo=@AgencyNo
        where ID=@ID
END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromaccount
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromaccount]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromaccount]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromaccount]        
AS
BEGIN
        Select * from DR_account
END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromaccountByAgencyNo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromaccountByAgencyNo]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromaccountByAgencyNo]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromaccountByAgencyNo]
        @AgencyNo [nvarchar](50)        
AS
BEGIN
        Select * from DR_account where AgencyNo=@AgencyNo
END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromaccountById
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromaccountById]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromaccountById]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromaccountById]
        @Id [int]        
AS
BEGIN
        Select * from DR_account where Id=@Id
END
GO


-- ----------------------------
-- Procedure structure for DR_UserLogin
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_UserLogin]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_UserLogin]
GO

CREATE PROCEDURE [dbo].[DR_UserLogin]
        @UserName nvarchar(32),
        @UserPassword nvarchar(128)
AS
BEGIN
        select count(ID)
        from DR_UserInformation
        where username=@UserName
        and userpassword=@UserPassword
END
GO


-- ----------------------------
-- Procedure structure for DR_InsertUserinformation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_InsertUserinformation]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_InsertUserinformation]
GO

CREATE PROCEDURE [dbo].[DR_InsertUserinformation]
        @ID int=null,
        @UserName nvarchar(50),
        @UserPassword nvarchar(50),
		@AgencyNo nvarchar(50)

AS
BEGIN
        Insert into DR_UserInformation
        (
                username,
                userpassword,
				AgencyNo

        )
        values
        (
                @UserName,
                @UserPassword,
				@AgencyNo

        )
        select @@identity as 'identity'
END
GO


-- ----------------------------
-- Procedure structure for DR_deleteaccount
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_deleteaccount]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_deleteaccount]
GO

CREATE PROCEDURE [dbo].[DR_deleteaccount]
        @ID int



AS
BEGIN
        delete from DR_account where Id=@ID
       

END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromUserinformation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromUserinformation]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromUserinformation]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromUserinformation]        
AS
BEGIN
        Select * from DR_UserInformation
END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromUserinformationById
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromUserinformationById]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromUserinformationById]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromUserinformationById]
        @UID int        
AS
BEGIN
        Select * from DR_UserInformation where Id=@UID
END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromUserinformationByName
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromUserinformationByName]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromUserinformationByName]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromUserinformationByName]
        @UName nvarchar(32)        
AS
BEGIN
        Select * from DR_UserInformation where username=@UName
END
GO


-- ----------------------------
-- Procedure structure for DR_UpdateUserinformation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_UpdateUserinformation]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_UpdateUserinformation]
GO

CREATE PROCEDURE [dbo].[DR_UpdateUserinformation]
        @ID int,
        @UserName nvarchar(50),
        @UserPassword nvarchar(50),
		@AgencyNo nvarchar(50)

AS
BEGIN
        Update DR_UserInformation
        SET 
username=@UserName,
            userpassword=@UserPassword,
			AgencyNo=@AgencyNo
        where Id=@ID
END
GO


-- ----------------------------
-- Procedure structure for DR_InsertAgencyinformation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_InsertAgencyinformation]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_InsertAgencyinformation]
GO

CREATE PROCEDURE [dbo].[DR_InsertAgencyinformation]
        @ID int=null,
        @AgencyNo nvarchar(50),
@account_bank nvarchar(50),
@account_bank_code nvarchar(50),
@banktype_code nvarchar(50),
@banktype_name nvarchar(50)
AS
BEGIN
        Insert into DR_AgencyInformation
        (
                AgencyNo,
account_bank,
account_bank_code,
banktype_code,
banktype_name
        )
        values
        (
                @AgencyNo,
@account_bank,
@account_bank_code,
@banktype_code,
@banktype_name

        )
        select @@identity as 'identity'
END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromAgencyInformation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromAgencyInformation]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromAgencyInformation]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromAgencyInformation]        
AS
BEGIN
        Select * from DR_AgencyInformation
END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromAgencyInformationByAgencyNo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromAgencyInformationByAgencyNo]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromAgencyInformationByAgencyNo]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromAgencyInformationByAgencyNo]
        @AgencyNo [nvarchar](50)        
AS
BEGIN
        Select * from DR_AgencyInformation where AgencyNo=@AgencyNo
END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromAgencyInformationById
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromAgencyInformationById]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromAgencyInformationById]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromAgencyInformationById]
        @Id [int]        
AS
BEGIN
        Select * from DR_AgencyInformation where Id=@Id
END
GO


-- ----------------------------
-- Procedure structure for DR_UpdateAgencyinformation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_UpdateAgencyinformation]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_UpdateAgencyinformation]
GO

CREATE PROCEDURE [dbo].[DR_UpdateAgencyinformation]
        @ID int,
        @AgencyNo nvarchar(50),
@account_bank nvarchar(50),
@account_bank_code nvarchar(50),
@banktype_code nvarchar(50),
@banktype_name nvarchar(50)
AS
BEGIN
        Update DR_AgencyInformation
        SET  AgencyNo=@AgencyNo,
account_bank=@account_bank,
account_bank_code=@account_bank_code,
banktype_code=@banktype_code,
banktype_name=@banktype_name
        where AgencyNo=@AgencyNo
END
GO


-- ----------------------------
-- Procedure structure for DR_InsertFTPinformation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_InsertFTPinformation]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_InsertFTPinformation]
GO

CREATE PROCEDURE [dbo].[DR_InsertFTPinformation]
        @ID int=null,
        @FTPAddress nvarchar(50),
		@FTPUsername nvarchar(50),
		@FTPPassword nvarchar(50),
		@data_Key nvarchar(50),
        @AgencyNo nvarchar(50)
AS
BEGIN
        Insert into DR_FTPInformation
        (
                 FTPAddress,
		FTPUsername,
		FTPPassword,
		data_Key,
        AgencyNo
        )
        values
        (
                 @FTPAddress,
		@FTPUsername,
		@FTPPassword,
		@data_Key,
        @AgencyNo

        )
        select @@identity as 'identity'
END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromFTPInformation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromFTPInformation]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromFTPInformation]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromFTPInformation]        
AS
BEGIN
        Select * from DR_FTPInformation
END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromFTPInformationByAgencyNo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromFTPInformationByAgencyNo]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromFTPInformationByAgencyNo]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromFTPInformationByAgencyNo]     
@AgencyNo [nvarchar](50)
AS
BEGIN
        Select * from DR_FTPInformation where AgencyNo=@AgencyNo
END
GO


-- ----------------------------
-- Procedure structure for DR_UpdateFTPinformation
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_UpdateFTPinformation]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_UpdateFTPinformation]
GO

CREATE PROCEDURE [dbo].[DR_UpdateFTPinformation]
        @ID int,
        @FTPAddress nvarchar(50),
		@FTPUsername nvarchar(50),
		@FTPPassword nvarchar(50),
		@data_Key nvarchar(50),
        @AgencyNo nvarchar(50)
AS
BEGIN
        update DR_FTPInformation
        set
                 FTPAddress=@FTPAddress,
		FTPUsername=@FTPUsername,
		FTPPassword=@FTPPassword,
		data_Key=@data_Key,
        AgencyNo=@AgencyNo
where Id=@ID
END
GO


-- ----------------------------
-- Procedure structure for DR_deletelogs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_deletelogs]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_deletelogs]
GO

CREATE PROCEDURE [dbo].[DR_deletelogs]

AS
BEGIN
       truncate table DR_logs
       

END
GO


-- ----------------------------
-- Procedure structure for DR_Insertdata
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_Insertdata]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_Insertdata]
GO

CREATE PROCEDURE [dbo].[DR_Insertdata]
        @ID int=null,
        @inputTime datetime,
		@data_Position nvarchar(50),
		@dataName nvarchar(50),
        @AgencyNo nvarchar(50),
@datatype nvarchar(50),
@upload nvarchar(50)
AS
BEGIN
        Insert into DR_data
        (
                 inputTime,
		data_position,
		dataName,
        AgencyNo,
datatype,
upload
        )
        values
        (
                @inputTime,
		@data_Position,
		@dataName,
        @AgencyNo,
@datatype,
@upload
        )
        select @@identity as 'identity'
END
GO


-- ----------------------------
-- Procedure structure for DR_deleteFTP
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_deleteFTP]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_deleteFTP]
GO

CREATE PROCEDURE [dbo].[DR_deleteFTP]
        @ID int



AS
BEGIN
        delete from DR_FTPInformation where Id=@ID
       

END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromdata
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromdata]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromdata]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromdata]        
AS
BEGIN
        Select * from DR_data
END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromdataByAgencyNo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromdataByAgencyNo]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromdataByAgencyNo]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromdataByAgencyNo]
        @AgencyNo [nvarchar](50)        
AS
BEGIN
        Select * from DR_data where AgencyNo=@AgencyNo
END
GO


-- ----------------------------
-- Procedure structure for DR_Updatedata
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_Updatedata]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_Updatedata]
GO

CREATE PROCEDURE [dbo].[DR_Updatedata]
        @ID int,
@inputTime datetime,
        @data_Position nvarchar(50),
@dataName nvarchar(50),
        @AgencyNo nvarchar(50),
@datatype nvarchar(50),
@upload nvarchar(50)
AS
BEGIN
        Update DR_data
        SET inputTime=@inputTime,
data_position=@data_Position,
dataName=@dataName,
            AgencyNo=@AgencyNo,
datatype=@datatype,
upload=@upload
        where ID=@ID
END
GO


-- ----------------------------
-- Procedure structure for DR_Insertlogs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_Insertlogs]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_Insertlogs]
GO

CREATE PROCEDURE [dbo].[DR_Insertlogs]
        @ID int=null,
@log_No nvarchar(50),
        @inputTime datetime,
		@succeed nvarchar(50),
		@logName nvarchar(50),
        @AgencyNo nvarchar(50)
AS
BEGIN
        Insert into DR_logs
        (
log_No,
                 inputTime,
		succeed,
		logName,
        AgencyNo
        )
        values
        (
@log_No,
                @inputTime,
		@succeed,
		@logName,
        @AgencyNo

        )
        select @@identity as 'identity'
END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromlogs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromlogs]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromlogs]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromlogs]        
AS
BEGIN
        Select * from DR_logs
END
GO


-- ----------------------------
-- Procedure structure for DR_SelectAllFromlogsByAgencyNo
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_SelectAllFromlogsByAgencyNo]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_SelectAllFromlogsByAgencyNo]
GO

CREATE PROCEDURE [dbo].[DR_SelectAllFromlogsByAgencyNo]
        @AgencyNo [nvarchar](50)        
AS
BEGIN
        Select * from DR_logs where AgencyNo=@AgencyNo
END
GO


-- ----------------------------
-- Procedure structure for DR_Updatelogs
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_Updatelogs]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_Updatelogs]
GO

CREATE PROCEDURE [dbo].[DR_Updatelogs]
        @ID int,
@log_No nvarchar(50),
@inputTime datetime,
        @succeed nvarchar(50),
@logName nvarchar(50),
        @AgencyNo nvarchar(50)
AS
BEGIN
        Update DR_logs
        SET log_No=@log_No,
inputTime=@inputTime,
succeed=@succeed,
logName=@logName,
            AgencyNo=@AgencyNo
        where ID=@ID
END
GO


-- ----------------------------
-- Procedure structure for DR_Insertaccount
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_Insertaccount]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_Insertaccount]
GO

CREATE PROCEDURE [dbo].[DR_Insertaccount]
        @ID int=null,
		@account_no nvarchar(50),
		@account_name nvarchar(50),
		@rg_code nvarchar(50),
		@type_code nvarchar(50),
		@type_name nvarchar(50),
        @AgencyNo nvarchar(50)


AS
BEGIN
        Insert into DR_account
        (
         account_no,
		account_name,
		rg_code,
		type_code,
		type_name,
        AgencyNo
        )
        values
        (
         @account_no,
		@account_name,
		@rg_code,
		@type_code,
		@type_name,
        @AgencyNo

        )
        select @@identity as 'identity'
END
GO


-- ----------------------------
-- Procedure structure for DR_deletedata
-- ----------------------------
IF EXISTS (SELECT * FROM sys.all_objects WHERE object_id = OBJECT_ID(N'[dbo].[DR_deletedata]') AND type IN ('P', 'PC', 'RF', 'X'))
	DROP PROCEDURE[dbo].[DR_deletedata]
GO

CREATE PROCEDURE [dbo].[DR_deletedata]
        @ID int

AS
BEGIN
delete from DR_data where Id=@ID       

END
GO


-- ----------------------------
-- Auto increment value for DR_account
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[DR_account]', RESEED, 79)
GO


-- ----------------------------
-- Primary Key structure for table DR_account
-- ----------------------------
ALTER TABLE [dbo].[DR_account] ADD CONSTRAINT [PK__tmp_ms_x__3214EC0797D03327] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for DR_admin
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[DR_admin]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table DR_admin
-- ----------------------------
ALTER TABLE [dbo].[DR_admin] ADD CONSTRAINT [PK__tmp_ms_x__3214EC07B6CC222F] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for DR_AgencyInformation
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[DR_AgencyInformation]', RESEED, 2)
GO


-- ----------------------------
-- Primary Key structure for table DR_AgencyInformation
-- ----------------------------
ALTER TABLE [dbo].[DR_AgencyInformation] ADD CONSTRAINT [PK__tmp_ms_x__3214EC073B9D6DF2] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for DR_data
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[DR_data]', RESEED, 206)
GO


-- ----------------------------
-- Primary Key structure for table DR_data
-- ----------------------------
ALTER TABLE [dbo].[DR_data] ADD CONSTRAINT [PK__tmp_ms_x__3214EC075FF089C9] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for DR_FTPInformation
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[DR_FTPInformation]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table DR_FTPInformation
-- ----------------------------
ALTER TABLE [dbo].[DR_FTPInformation] ADD CONSTRAINT [PK__tmp_ms_x__3214EC070595A89A] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for DR_import_logs
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[DR_import_logs]', RESEED, 199)
GO


-- ----------------------------
-- Primary Key structure for table DR_import_logs
-- ----------------------------
ALTER TABLE [dbo].[DR_import_logs] ADD CONSTRAINT [PK__tmp_ms_x__3214EC070EFEFD06] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for DR_logs
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[DR_logs]', RESEED, 1)
GO


-- ----------------------------
-- Primary Key structure for table DR_logs
-- ----------------------------
ALTER TABLE [dbo].[DR_logs] ADD CONSTRAINT [PK__DR_logs__3214EC07B31BCBE9] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO


-- ----------------------------
-- Auto increment value for DR_UserInformation
-- ----------------------------
DBCC CHECKIDENT ('[dbo].[DR_UserInformation]', RESEED, 2)
GO


-- ----------------------------
-- Primary Key structure for table DR_UserInformation
-- ----------------------------
ALTER TABLE [dbo].[DR_UserInformation] ADD CONSTRAINT [PK__tmp_ms_x__3214EC07ED1EED83] PRIMARY KEY CLUSTERED ([Id])
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)  
ON [PRIMARY]
GO

