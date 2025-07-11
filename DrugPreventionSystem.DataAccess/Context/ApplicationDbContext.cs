using DrugPreventionSystem.DataAccess.Utilities;
using DrugPreventionSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DrugPreventionSystem.DataAccess.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSets
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Consultant> Consultants { get; set; }
        public DbSet<Survey> Surveys { get; set; } = null!;
        public DbSet<SurveyQuestion> SurveyQuestions { get; set; } = null!;
        public DbSet<SurveyOption> SurveyOptions { get; set; } = null!;
        public DbSet<UserSurveyResponse> UserSurveyResponses { get; set; } = null!;
        public DbSet<UserSurveyAnswer> UserSurveyAnswers { get; set; } = null!;
        public DbSet<Instructor> Instructors { get; set; } = null!;
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<CourseWeek> CourseWeeks { get; set; } = null!;
        public DbSet<Lesson> Lessons { get; set; } = null!;
        public DbSet<LessonResource> LessonResources { get; set; } = null!;
        public DbSet<Quiz> Quizzes { get; set; } = null!;
        public DbSet<QuizQuestion> QuizQuestions { get; set; } = null!;
        public DbSet<QuizOption> QuizOptions { get; set; } = null!;
        public DbSet<PracticeExercise> PracticeExercises { get; set; } = null!;
        public DbSet<UserLessonProgress> UserLessonProgresses { get; set; } = null!;
        public DbSet<UserQuizAnswer> UserQuizAnswers { get; set; } = null!;
        public DbSet<UserModuleQuizResult> UserModuleQuizResults { get; set; } = null!;
        public DbSet<CourseCertificate> CourseCertificates { get; set; } = null!;
        public DbSet<SurveyCourseRecommendation> SurveyCourseRecommendations { get; set; } = null!;
        public DbSet<UserResponseCourseRecommendation> UserResponseCourseRecommendations { get; set; } = null!;
        public DbSet<UserCourseEnrollment> UserCourseEnrollments { get; set; } = null!;
        public DbSet<CommunityProgram> CommunityPrograms { get; set; } = null!;
        public DbSet<ProgramParticipant> ProgramParticipants { get; set; } = null!;
        public DbSet<ProgramFeedback> ProgramFeedbacks { get; set; } = null!;
        public DbSet<TimeSlot> TimeSlots { get; set; } = null!;
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Blog> Blogs { get; set; } = null!; 
        public DbSet<BlogCategory> BlogCategories { get; set; } = null!; 
        public DbSet<ProgramSurvey> ProgramSurveys { get; set; } = null!;
        public DbSet<ProgramSurveyQuestion> ProgramSurveyQuestions { get; set; } = null!;
        public DbSet<ProgramSurveyAnswerOption> ProgramSurveyAnswerOptions { get; set; } = null!;
        public DbSet<ProgramSurveyResponse> ProgramSurveyResponses { get; set; } = null!;
        public DbSet<ProgramSurveyAnswer> ProgramSurveyAnswers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.User)
                .WithOne(u => u.UserProfile)
                .HasForeignKey<UserProfile>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Consultant>()
                .HasOne(c => c.User)
                .WithOne(u => u.Consultant)
                .HasForeignKey<Consultant>(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure indexes
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Configure decimal precision
            modelBuilder.Entity<Consultant>()
                .Property(c => c.ConsultationFee)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Consultant>()
                .Property(c => c.Rating)
                .HasColumnType("decimal(3,2)");

            // Survey có nhiều SurveyQuestions
            modelBuilder.Entity<SurveyQuestion>()
                .HasOne(sq => sq.Survey)
                .WithMany(s => s.SurveyQuestions)
                .HasForeignKey(sq => sq.SurveyId)
                .OnDelete(DeleteBehavior.Cascade); // Khi xóa khảo sát, các câu hỏi của nó cũng bị xóa

            // SurveyQuestion có nhiều SurveyOptions
            modelBuilder.Entity<SurveyOption>()
                .HasOne(so => so.SurveyQuestion)
                .WithMany(sq => sq.SurveyOptions)
                .HasForeignKey(so => so.QuestionId)
                .OnDelete(DeleteBehavior.Cascade); // Khi xóa câu hỏi, các lựa chọn của nó cũng bị xóa

            // UserSurveyResponse liên kết với User và Survey
            modelBuilder.Entity<UserSurveyResponse>()
                .HasOne(usr => usr.User)
                .WithMany(u => u.UserSurveyResponses) 
                .HasForeignKey(usr => usr.UserId)
                .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<UserSurveyResponse>()
                .HasOne(usr => usr.Survey)
                .WithMany(s => s.UserSurveyResponses)
                .HasForeignKey(usr => usr.SurveyId)
                .OnDelete(DeleteBehavior.Cascade); 

            // UserSurveyAnswer liên kết với UserSurveyResponse, SurveyQuestion và SurveyOption
            modelBuilder.Entity<UserSurveyAnswer>()
                .HasOne(usa => usa.UserSurveyResponse)
                .WithMany(usr => usr.UserSurveyAnswers)
                .HasForeignKey(usa => usa.ResponseId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<UserSurveyAnswer>()
                .HasOne(usa => usa.SurveyQuestion)
                .WithMany(sq => sq.UserSurveyAnswers)
                .HasForeignKey(usa => usa.QuestionId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<UserSurveyAnswer>()
                .HasOne(usa => usa.SurveyOption)
                .WithMany(so => so.UserSurveyAnswers)
                .HasForeignKey(usa => usa.OptionId)
                .OnDelete(DeleteBehavior.Restrict);
            // Instructor có nhiều Courses
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Instructor)
                .WithMany(i => i.Courses)
                .HasForeignKey(c => c.InstructorId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa Instructor nếu còn Course

            // Course có nhiều CourseWeeks
            modelBuilder.Entity<CourseWeek>()
                .HasOne(cw => cw.Course)
                .WithMany(c => c.CourseWeeks)
                .HasForeignKey(cw => cw.CourseId)
                .OnDelete(DeleteBehavior.Cascade); // Khi xóa Course, Weeks cũng bị xóa

            // CourseWeek có nhiều Lessons
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.CourseWeek)
                .WithMany(cw => cw.Lessons)
                .HasForeignKey(l => l.WeekId)
                .OnDelete(DeleteBehavior.Cascade); // Khi xóa Week, Lessons cũng bị xóa

            // Lesson có nhiều LessonResources
            modelBuilder.Entity<LessonResource>()
                .HasOne(lr => lr.Lesson)
                .WithMany(l => l.LessonResources)
                .HasForeignKey(lr => lr.LessonId)
                .OnDelete(DeleteBehavior.Cascade); // Khi xóa Lesson, Resources cũng bị xóa

            // Lesson có một Quiz (One-to-One)
            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.Lesson)
                .WithOne(l => l.Quiz)
                .HasForeignKey<Quiz>(q => q.LessonId)
                .OnDelete(DeleteBehavior.Cascade); // Khi xóa Lesson, Quiz cũng bị xóa

            // Quiz có nhiều QuizQuestions
            modelBuilder.Entity<QuizQuestion>()
                .HasOne(qq => qq.Quiz)
                .WithMany(q => q.QuizQuestions)
                .HasForeignKey(qq => qq.QuizId)
                .OnDelete(DeleteBehavior.Cascade); // Khi xóa Quiz, Questions cũng bị xóa

            // QuizQuestion có nhiều QuizOptions
            modelBuilder.Entity<QuizOption>()
                .HasOne(qo => qo.QuizQuestion)
                .WithMany(qq => qq.QuizOptions)
                .HasForeignKey(qo => qo.QuestionId)
                .OnDelete(DeleteBehavior.Cascade); // Khi xóa Question, Options cũng bị xóa

            // Lesson có nhiều PracticeExercises
            modelBuilder.Entity<PracticeExercise>()
                .HasOne(pe => pe.Lesson)
                .WithMany(l => l.PracticeExercises)
                .HasForeignKey(pe => pe.LessonId)
                .OnDelete(DeleteBehavior.Cascade); // Khi xóa Lesson, PracticeExercises cũng bị xóa

            // UserLessonProgress liên kết với User và Lesson
            modelBuilder.Entity<UserLessonProgress>()
                .HasOne(ulp => ulp.User)
                .WithMany(u => u.UserLessonProgresses)
                .HasForeignKey(ulp => ulp.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa User nếu còn tiến độ bài học
            modelBuilder.Entity<UserLessonProgress>()
                .HasOne(ulp => ulp.Lesson)
                .WithMany(l => l.UserLessonProgresses)
                .HasForeignKey(ulp => ulp.LessonId)
                .OnDelete(DeleteBehavior.Cascade); // Khi xóa Lesson, tiến độ cũng bị xóa

            // UserQuizAnswer liên kết với User, QuizQuestion, QuizOption
            modelBuilder.Entity<UserQuizAnswer>()
                .HasOne(uqa => uqa.User)
                .WithMany(u => u.UserQuizAnswers)
                .HasForeignKey(uqa => uqa.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserQuizAnswer>()
                .HasOne(uqa => uqa.QuizQuestion)
                .WithMany(qq => qq.UserQuizAnswers)
                .HasForeignKey(uqa => uqa.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserQuizAnswer>()
                .HasOne(uqa => uqa.SelectedOption)
                .WithMany(qo => qo.UserQuizAnswers)
                .HasForeignKey(uqa => uqa.SelectedOptionId)
                .OnDelete(DeleteBehavior.Restrict);

            // UserModuleQuizResult liên kết với User và Lesson
            modelBuilder.Entity<UserModuleQuizResult>()
                .HasOne(umqr => umqr.User)
                .WithMany(u => u.UserModuleQuizResults) // Cần thêm Navigation Property này vào User
                .HasForeignKey(umqr => umqr.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserModuleQuizResult>()
                .HasOne(umqr => umqr.Lesson)
                .WithMany(l => l.UserModuleQuizResults) // Cần thêm Navigation Property này vào Lesson
                .HasForeignKey(umqr => umqr.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            // CourseCertificate liên kết với User và Course
            modelBuilder.Entity<CourseCertificate>()
                .HasOne(cc => cc.User)
                .WithMany(u => u.CourseCertificates) // Cần thêm Navigation Property này vào User
                .HasForeignKey(cc => cc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CourseCertificate>()
                .HasOne(cc => cc.Course)
                .WithMany(c => c.CourseCertificates)
                .HasForeignKey(cc => cc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);


            // Cấu hình Mối quan hệ cho Đề xuất khóa học từ khảo sát
            modelBuilder.Entity<SurveyCourseRecommendation>()
                .HasOne(scr => scr.Survey)
                .WithMany(s => s.SurveyCourseRecommendations)
                .HasForeignKey(scr => scr.SurveyId)
                .OnDelete(DeleteBehavior.Cascade); // Khi Survey bị xóa, các quy tắc đề xuất cũng bị xóa

            modelBuilder.Entity<SurveyCourseRecommendation>()
                .HasOne(scr => scr.Course)
                .WithMany(c => c.SurveyCourseRecommendations)
                .HasForeignKey(scr => scr.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa Course nếu còn được đề xuất trong quy tắc

            modelBuilder.Entity<UserResponseCourseRecommendation>()
                .HasOne(urcr => urcr.UserSurveyResponse)
                .WithMany(usr => usr.UserResponseCourseRecommendations) // Cần thêm Navigation Property này vào UserSurveyResponse
                .HasForeignKey(urcr => urcr.ResponseId)
                .OnDelete(DeleteBehavior.Cascade); // Khi UserSurveyResponse bị xóa, các đề xuất liên quan cũng bị xóa

            modelBuilder.Entity<UserResponseCourseRecommendation>()
                .HasOne(urcr => urcr.Course)
                .WithMany(c => c.UserResponseCourseRecommendations)
                .HasForeignKey(urcr => urcr.CourseId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa Course nếu đã được đề xuất cho người dùng

            modelBuilder.Entity<UserCourseEnrollment>()
                .Property(e => e.Status)
                .HasConversion<string>(); // Lưu enum dưới dạng chuỗi trong DB

            
            modelBuilder.Entity<UserCourseEnrollment>()
                .HasIndex(uce => new { uce.UserId, uce.CourseId })
                .IsUnique();

            // Cấu hình mối quan hệ 1-nhiều giữa User và UserCourseEnrollment
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserCourseEnrollments)
                .WithOne(uce => uce.User)
                .HasForeignKey(uce => uce.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Cấu hình mối quan hệ 1-nhiều giữa Course và UserCourseEnrollment
            modelBuilder.Entity<Course>()
                .HasMany(c => c.UserCourseEnrollments)
                .WithOne(uce => uce.Course)
                .HasForeignKey(uce => uce.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // CommunityProgram và ProgramParticipant (Một chương trình có nhiều người tham gia)
            modelBuilder.Entity<CommunityProgram>()
                .HasMany(cp => cp.ProgramParticipants)
                .WithOne(pp => pp.CommunityProgram)
                .HasForeignKey(pp => pp.ProgramId)
                .OnDelete(DeleteBehavior.Cascade); 

            // User và ProgramParticipant (Một người dùng có thể tham gia nhiều chương trình)
            modelBuilder.Entity<User>() 
                .HasMany(u => u.ProgramParticipants)
                .WithOne(pp => pp.User)
                .HasForeignKey(pp => pp.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            // CommunityProgram và ProgramFeedback (Một chương trình có nhiều feedback)
            modelBuilder.Entity<CommunityProgram>()
                .HasMany(cp => cp.ProgramFeedbacks)
                .WithOne(pf => pf.CommunityProgram)
                .HasForeignKey(pf => pf.ProgramId)
                .OnDelete(DeleteBehavior.Cascade); 

            // User và ProgramFeedback (Một người dùng có thể gửi nhiều feedback)
            modelBuilder.Entity<User>() 
                .HasMany(u => u.ProgramFeedbacks)
                .WithOne(pf => pf.User)
                .HasForeignKey(pf => pf.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Không xóa user khi xóa feedback của họ

            // TimeSlot configurations
            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.HasIndex(ts => new { ts.ConsultantId, ts.SlotDate, ts.StartTime })
                      .IsUnique();

                entity.HasOne(ts => ts.Consultant)
                      .WithMany(c => c.TimeSlots)
                      .HasForeignKey(ts => ts.ConsultantId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Appointment configurations
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasOne(a => a.User)
                      .WithMany(u => u.Appointments)
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.Consultant)
                      .WithMany(c => c.Appointments)
                      .HasForeignKey(a => a.ConsultantId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(a => a.TimeSlot)
                      .WithOne(ts => ts.Appointment)
                      .HasForeignKey<Appointment>(a => a.TimeSlotId)
                      .IsRequired()
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(a => a.TimeSlotId)
                      .IsUnique();

            });

            // BlogCategory configurations
            modelBuilder.Entity<BlogCategory>(entity =>
            {
                entity.HasIndex(bc => bc.Name)
                      .IsUnique(); // Ensure category names are unique
            });

            // Blog configurations
            modelBuilder.Entity<Blog>(entity =>
            {
                // Relationship: BlogCategory (One) to Blogs (Many)
                entity.HasOne(b => b.Category)
                      .WithMany(bc => bc.Blogs)
                      .HasForeignKey(b => b.CategoryId)
                      .IsRequired(false) // CategoryId is nullable
                      .OnDelete(DeleteBehavior.SetNull); // Set CategoryId to NULL if category is deleted

               // Relationship: User (One) to Blogs (Many)
                entity.HasOne(b => b.User)
                      .WithMany(u => u.Blogs)
                      .HasForeignKey(b => b.UserId)
                      .IsRequired();
            });

            // Cấu hình mối quan hệ cho ProgramSurvey
            modelBuilder.Entity<ProgramSurvey>(entity =>
            {
                entity.HasMany(s => s.Questions)
                      .WithOne(q => q.ProgramSurvey)
                      .HasForeignKey(q => q.SurveyId)
                      .OnDelete(DeleteBehavior.Cascade); // Xóa các câu hỏi khi khảo sát bị xóa
            });

            // Cấu hình mối quan hệ cho ProgramSurveyQuestion
            modelBuilder.Entity<ProgramSurveyQuestion>(entity =>
            {
                entity.HasOne(q => q.ProgramSurvey)
                      .WithMany(s => s.Questions)
                      .HasForeignKey(q => q.SurveyId)
                      .OnDelete(DeleteBehavior.Restrict); // Giữ lại khảo sát nếu có câu hỏi
                entity.HasMany(q => q.AnswerOptions)
                      .WithOne(a => a.ProgramSurveyQuestion)
                      .HasForeignKey(a => a.QuestionId)
                      .OnDelete(DeleteBehavior.Cascade); // Xóa các tùy chọn trả lời khi câu hỏi bị xóa
            });

            // Cấu hình mối quan hệ cho ProgramSurveyAnswerOption
            modelBuilder.Entity<ProgramSurveyAnswerOption>(entity =>
            {
                entity.HasOne(a => a.ProgramSurveyQuestion)
                      .WithMany(q => q.AnswerOptions)
                      .HasForeignKey(a => a.QuestionId)
                      .OnDelete(DeleteBehavior.Restrict); // Giữ lại câu hỏi nếu có tùy chọn
            });

            // Cấu hình mối quan hệ cho CommunityProgram với ProgramSurvey (1-to-N hoặc 0-to-N)
            modelBuilder.Entity<CommunityProgram>(entity =>
            {
                entity.HasOne(cp => cp.ProgramSurvey)
                      .WithMany(ps => ps.Programs)
                      .HasForeignKey(cp => cp.SurveyId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Cấu hình mối quan hệ cho ProgramSurveyResponse
            modelBuilder.Entity<ProgramSurveyResponse>(entity =>
            {
                entity.HasOne(r => r.ProgramSurvey)
                      .WithMany()
                      .HasForeignKey(r => r.SurveyId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(r => r.CommunityProgram)
                      .WithMany()
                      .HasForeignKey(r => r.ProgramId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(r => r.Answers)
                      .WithOne(a => a.ProgramSurveyResponse)
                      .HasForeignKey(a => a.ResponseId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Cấu hình mối quan hệ cho ProgramSurveyAnswer
            modelBuilder.Entity<ProgramSurveyAnswer>(entity =>
            {
                entity.HasOne(a => a.ProgramSurveyResponse)
                      .WithMany(r => r.Answers)
                      .HasForeignKey(a => a.ResponseId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.ProgramSurveyQuestion)
                      .WithMany()
                      .HasForeignKey(a => a.QuestionId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.ProgramSurveyAnswerOption)
                      .WithMany()
                      .HasForeignKey(a => a.SelectedOptionId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);
            });


            // Seed initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = 1, RoleName = "Admin", Description = "System Administrator" },
                new Role { RoleId = 2, RoleName = "Manager", Description = "System Manager" },
                new Role { RoleId = 3, RoleName = "Staff", Description = "Staff Member" },
                new Role { RoleId = 4, RoleName = "Consultant", Description = "Professional Consultant" },
                new Role { RoleId = 5, RoleName = "Member", Description = "Registered Member" }
            );

            var hashedPassword = PasswordHasher.HashPassword("12345");
            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = Guid.NewGuid(), // Tạo GUID mới
                    Username = "admin_user",
                    Email = "admin@example.com",
                    PasswordHash = hashedPassword,
                    RoleId = 1, // Admin role
                    IsActive = true,
                    EmailVerified = true,
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    UserId = Guid.NewGuid(), // Tạo GUID mới
                    Username = "manager_user",
                    Email = "manager@example.com",
                    PasswordHash = hashedPassword,
                    RoleId = 2, // Manager role
                    IsActive = true,
                    EmailVerified = true,
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    UserId = Guid.NewGuid(), // Tạo GUID mới
                    Username = "staff_user",
                    Email = "staff@example.com",
                    PasswordHash = hashedPassword,
                    RoleId = 3, // Staff role
                    IsActive = true,
                    EmailVerified = true,
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    UserId = Guid.NewGuid(), // Tạo GUID mới
                    Username = "consultant_user",
                    Email = "consultant@example.com",
                    PasswordHash = hashedPassword,
                    RoleId = 4, // Consultant role
                    IsActive = true,
                    EmailVerified = true,
                    CreatedAt = DateTime.Now
                }
            );
        }
    }
}
