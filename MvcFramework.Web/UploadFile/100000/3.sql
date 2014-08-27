-- �ý��� �¾� ( APPCONFIG )
CREATE TABLE COM_SYSTEM_SETUP
(
	SYS_ID				INT	PRIMARY KEY IDENTITY(1,1),	--�ý��۾��̵�
	SYS_KEY				NVARCHAR(50),					--Ű
	SYS_VALUE			NVARCHAR(100),					--��
	SYS_DESC			NVARCHAR(500),					--����
	CREATE_DATE			DATETIME,						--������
	CREATE_ID			INT,							--������
	UPDATE_DATE			DATETIME,						--������
	UPDATE_ID			INT								--������
)
--�ٱ���
CREATE TABLE COM_LANGUAGE_INFO
(
	LANG_ID				INT PRIMARY KEY IDENTITY(1,1),	--�����̵�
	LANG_TYPE			VARCHAR(1),						--���Ÿ��(L:��, M:�޼���)
	LANG_KO				NVARCHAR(500),					--�ѱ���
	LANG_EN				NVARCHAR(500),					--����
	LANG_JP				NVARCHAR(500),					--�Ͼ�
	LANG_CH				NVARCHAR(500),					--�߱���
	CREATE_DATE			DATETIME,						--������
	CREATE_ID			INT,							--������
	UPDATE_DATE			DATETIME,						--������
	UPDATE_ID			INT								--������
)
GO
--�ڵ�ī�װ�������
CREATE TABLE COM_CATEGORY_INFO
(
	CATEGORY_ID			VARCHAR(6) PRIMARY KEY,			--ī�װ� ���̵�
	CATEGORY_NAME		NVARCHAR(100),					--ī�װ���
	CATEGORY_DESC		NVARCHAR(500),					--ī�װ�����
	CREATE_DATE			DATETIME,						--������
	CREATE_ID			INT,							--������
	UPDATE_DATE			DATETIME,						--������
	UPDATE_ID			INT								--������
)
GO
--�ڵ帶����
CREATE TABLE COM_CODE_INFO
(
	CODE_ID			INT PRIMARY KEY IDENTITY(1,1),	--�ڵ� ���̵�
	CATEGORY_ID		VARCHAR(6),						--ī�װ� ���̵�
	ETC_CODE		VARCHAR(6),						--�ڵ�
	CODE_NAME_KR	NVARCHAR(100),					--�ڵ��
	CODE_NAME_EN	NVARCHAR(100),					--�ڵ��
	CODE_NAME_JP	NVARCHAR(100),					--�ڵ��
	CODE_NAME_CH	NVARCHAR(100),					--�ڵ��
	CODE_DESC		NVARCHAR(500),					--�ڵ弳��
	SORT_ORDER		INT,							--����
	CREATE_DATE		DATETIME,						--������
	CREATE_ID		INT,							--������
	UPDATE_DATE		DATETIME,						--������
	UPDATE_ID		INT								--������
)
GO
--����ڸ�����
CREATE TABLE COM_EMP_INFO
(
	EMP_REF_ID			INT PRIMARY KEY,	--�ý��۾��̵�
	LOGINID				VARCHAR(50),		--�α���
	LOGINPWD			VARCHAR(50),		--���
	EMP_NAME			NVARCHAR(50),		--�̸�
	EMP_EMAIL			VARCHAR(150),		--�̸���
	DEPT_REF_ID			INT,				--�μ����̵�
	POSITION_CLASS_CODE	VARCHAR(6),				--����(POS001)
	POSITION_GRP_CODE	VARCHAR(6),				--����(POS002)
	POSITION_RANK_CODE	VARCHAR(6),				--����(POS003)
	POSITION_DUTY_CODE	VARCHAR(6),				--��å(POS004)
	EMP_TELL			VARCHAR(50),		--����
	EMP_TYPE			VARCHAR(6),			--Ÿ��(EMP001)
	EMP_LAN_TYPE		VARCHAR(3),			--���(KOR,ENG,JPN,CHN)
	CREATE_DATE			DATETIME,			--������
	CREATE_ID			INT,				--������
	UPDATE_DATE			DATETIME,			--������
	UPDATE_ID			INT					--������
)
GO
--�μ�������
CREATE TABLE COM_DEPT_INFO
(
	DEPT_REF_ID			INT PRIMARY KEY,	--�μ����̵�
	UP_DEPT_ID			INT,				--�����μ�
	DEPT_LEVEL			INT,				--����
	DEPT_CODE			VARCHAR(50),		--�μ��ڵ�
	DEPT_NAME			NVARCHAR(100),		--�μ���
	USE_YN				VARCHAR(1),			--�������
	CREATE_DATE			DATETIME,			--������
	CREATE_ID			INT,				--������
	UPDATE_DATE			DATETIME,			--������
	UPDATE_ID			INT					--������
)
GO
--���Ѹ�����
CREATE TABLE COM_ROLE_INFO
(
	ROLE_ID				INT PRIMARY KEY IDENTITY(1,1),	--���Ѿ��̵�
	ROLE_KO_NAME		VARCHAR(100),					--���Ѹ�
	ROLE_JP_NAME		VARCHAR(100),					--���Ѹ�
	ROLE_EN_NAME		VARCHAR(100),					--���Ѹ�
	ROLE_CH_NAME		VARCHAR(100),					--���Ѹ�
	ROLE_SELECT			VARCHAR(1),						--SELECT����
	ROLE_DELETE			VARCHAR(1),						--DELETE����
	ROLE_UPDATE			VARCHAR(1),						--UPDATE����
	ROLE_INSERT			VARCHAR(1),						--INSERT����
	CREATE_DATE			DATETIME,			--������
	CREATE_ID			INT,				--������
	UPDATE_DATE			DATETIME,			--������
	UPDATE_ID			INT					--������
)
GO
--������������
CREATE TABLE COM_PAGE_INFO
(
	PAGE_ID				INT PRIMARY KEY IDENTITY(1,1),	--���������̵�
	PAGE_NAME_K0		NVARCHAR(100),					--��������
	PAGE_URL			VARCHAR(150),					--URL
	PAGE_IMG			VARCHAR(150),					--�̹���NAME
	CREATE_DATE			DATETIME,			--������
	CREATE_ID			INT,				--������
	UPDATE_DATE			DATETIME,			--������
	UPDATE_ID			INT					--������
)
GO
--�޴�������
CREATE TABLE COM_MENU_INFO
(
	MENU_ID				INT PRIMARY KEY IDENTITY(1,1),	--�޴����̵�
	UP_MENU_ID			INT,							--�����޴����̵�
	PAGE_ID				INT,							--���������̵�
	MENU_NAME_KO		NVARCHAR(100),					--�޴���
	MENU_NAME_JP		NVARCHAR(100),					--�޴���
	MENU_NAME_EN		NVARCHAR(100),					--�޴���
	MENU_NAME_CH		NVARCHAR(100),					--�޴���
	USE_YN				VARCHAR(1),						--��뿩��
	CREATE_DATE			DATETIME,			--������
	CREATE_ID			INT,				--������
	UPDATE_DATE			DATETIME,			--������
	UPDATE_ID			INT					--������
)
GO
--���Ѹ޴�
CREATE TABLE COM_ROLE_MENU
(
	ROLE_ID				INT,	--���Ѿ��̵�
	MENU_ID				INT,	--�޴����̵�
	CREATE_DATE			DATETIME,			--������
	CREATE_ID			INT,				--������
	UPDATE_DATE			DATETIME,			--������
	UPDATE_ID			INT					--������
)
--��������
CREATE TABLE COM_ROLE_USER
(
	ROLE_ID				INT,	--���Ѿ��̵�
	EMP_REF_ID			INT,	--����ھ��̵�
	CREATE_DATE			DATETIME,			--������
	CREATE_ID			INT,				--������
	UPDATE_DATE			DATETIME,			--������
	UPDATE_ID			INT					--������
)