using Microsoft.EntityFrameworkCore.Migrations;

namespace RxCats.DbMigration.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE TABLE `tb_user` (
  `user_id` bigint(20) NOT NULL AUTO_INCREMENT COMMENT '유저 고유아이디',
  `user_platform_id` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT 'firebase 유저아이디',
  `nickname` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL COMMENT '닉네임',
  `photo_url` varchar(500) COLLATE utf8mb4_unicode_ci DEFAULT NULL COMMENT '프로필사진URL',
  `provider_id` varchar(20) COLLATE utf8mb4_unicode_ci DEFAULT NULL COMMENT '인증 제공업체 아이디',
  `provider_user_id` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL COMMENT '인증 제공업체 유저아이디',
  `provider_name` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL COMMENT '인증 제공업체 유저이름',
  `provider_email` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL COMMENT '인증 제공업체 이메일',
  `created_datetime` datetime DEFAULT NULL COMMENT '생성일',
  `updated_datetime` datetime DEFAULT NULL COMMENT '갱신일',
  PRIMARY KEY (`user_id`),
  UNIQUE KEY `i_user_pf_id` (`user_platform_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
            ");

            migrationBuilder.Sql(@"
CREATE TABLE `tb_user_session` (
  `user_id` bigint(20) NOT NULL COMMENT '유저 고유아이디',
  `access_token` text COLLATE utf8mb4_unicode_ci COMMENT 'firebase 인증 토큰',
  `expiry_datetime` datetime DEFAULT NULL COMMENT '인증 만료일',
  `api_path` varchar(100) COLLATE utf8mb4_unicode_ci DEFAULT NULL COMMENT '마지막 접근 API',
  `ip_address` varchar(50) COLLATE utf8mb4_unicode_ci DEFAULT NULL COMMENT '마지막 접근 IP',
  `created_datetime` datetime DEFAULT NULL COMMENT '생성일',
  `updated_datetime` datetime DEFAULT NULL COMMENT '갱신일',
  PRIMARY KEY (`user_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
