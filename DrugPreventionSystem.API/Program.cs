using DrugPreventionSystem.BusinessLogic.Services;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces;
using DrugPreventionSystem.BusinessLogic.Services.Interfaces.Quizzes;
using DrugPreventionSystem.BusinessLogic.Services.Quizzes;
using DrugPreventionSystem.BusinessLogic.Token;
using DrugPreventionSystem.DataAccess.Context;
using DrugPreventionSystem.DataAccess.Repositories;
using DrugPreventionSystem.DataAccess.Repositories.Interfaces;
using DrugPreventionSystem.DataAccess.Repository;
using DrugPreventionSystem.DataAccess.Repository.Interfaces;
using DrugPreventionSystem.DataAccess.Repository.Interfaces.IQuizzes;
using DrugPreventionSystem.DataAccess.Repository.Participants;
using DrugPreventionSystem.DataAccess.Repository.Quizzes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT set up
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DrugPrevention", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Please Enter The Token To Authenticate The Role",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// Token
var serect = builder.Configuration["AppSettings:SecretKey"];
var key = Encoding.ASCII.GetBytes(serect);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

//  Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IConsultantRepository, ConsultantRepository>();
builder.Services.AddScoped<ISurveyOptionRepository, SurveyOptionRepository>();
builder.Services.AddScoped<ISurveyQuestionRepository, SurveyQuestionRepository>();
builder.Services.AddScoped<ISurveyRepository, SurveyRepository>();
builder.Services.AddScoped<IUserSurveyResponseRepository, UserSurveyResponseRepository>();
builder.Services.AddScoped<IUserSurveyAnswerRepository, UserSurveyAnswerRepository>();
builder.Services.AddScoped<ISurveyCourseRecommendationRepository, SurveyCourseRecommendationRepository>();
builder.Services.AddScoped<IUserResponseCourseRecommendationRepository, UserResponseCourseRecommendationRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IInstructorRepository, InstructorRepository>();
builder.Services.AddScoped<ICourseWeekRepository, CourseWeekRepository>();
builder.Services.AddScoped<ILessonRepository, LessonRepository>();
builder.Services.AddScoped<ILessonResourceRepository, LessonResourceRepository>();
builder.Services.AddScoped<IPracticeExerciseRepository, PracticeExerciseRepository>();
builder.Services.AddScoped<ICourseCertificateRepository, CourseCertificateRepository>();
builder.Services.AddScoped<IUserCourseEnrollmentRepository, UserCourseEnrollmentRepository>();
builder.Services.AddScoped<IUserQuizAnswerRepository, UserQuizAnswerRepository>();
builder.Services.AddScoped<IUserLessonProgressRepository, UserLessonProgressRepository>();
builder.Services.AddScoped<IUserModuleQuizResultRepository, UserModuleQuizResultRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuizQuestionRepository, QuizQuestionRepository>();
builder.Services.AddScoped<IQuizOptionRepository, QuizOptionRepository>();
builder.Services.AddScoped<IProgramParticipantRepository, ProgramParticipantRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
builder.Services.AddScoped<IProgramSurveyAnswerOptionRepository, ProgramSurveyAnswerOptionRepository>();
builder.Services.AddScoped<IProgramSurveyAnswerRepository, ProgramSurveyAnswerRepository>();
builder.Services.AddScoped<IProgramSurveyQuestionRepository, ProgramSurveyQuestionRepository>();
builder.Services.AddScoped<IProgramSurveyResponseRepository, ProgramSurveyResponseRepository>();
builder.Services.AddScoped<IProgramSurveyRepository, ProgramSurveyRepository>();




builder.Services.AddScoped<DrugPreventionSystem.DataAccess.Repository.Interfaces.IProgramFeedbackRepository, DrugPreventionSystem.DataAccess.Repository.ProgramFeedbackRepository>();
builder.Services.AddScoped<ICommunityProgramRepository, CommunityProgramRepository>();

//service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserProfileService, UserProfileService>();
builder.Services.AddScoped<IConsultantService, ConsultantService>();
builder.Services.AddScoped<ISurveyOptionService, SurveyOptionService>();
builder.Services.AddScoped<ISurveyQuestionService, SurveyQuestionService>();
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<IUserSurveyResponseService, UserSurveyResponseService>();
builder.Services.AddScoped<IUserSurveyAnswerService, UserSurveyAnswerService>();
builder.Services.AddScoped<ISurveyCourseRecommendationService, SurveyCourseRecommendationService>();
builder.Services.AddScoped<IUserResponseCourseRecommendationService, UserResponseCourseRecommendationService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IInstructorService, InstructorService>();
builder.Services.AddScoped<ICourseWeekService, CourseWeekService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<ILessonResourceService, LessonResourceService>();
builder.Services.AddScoped<IPracticeExerciseService, PracticeExerciseService>();
builder.Services.AddScoped<ICourseCertificateService, CourseCertificateService>();
builder.Services.AddScoped<IUserCourseEnrollmentService, UserCourseEnrollmentService>();
builder.Services.AddScoped<IUserQuizAnswerService, UserQuizAnswerService>();
builder.Services.AddScoped<IUserLessonProgressService, UserLessonProgressService>();
builder.Services.AddScoped<IUserModuleQuizResultService, UserModuleQuizResultService>();
builder.Services.AddScoped<IQuizService, QuizService>();
builder.Services.AddScoped<IQuizQuestionService, QuizQuestionService>();
builder.Services.AddScoped<IQuizOptionService, QuizOptionService>();
builder.Services.AddScoped<DrugPreventionSystem.BusinessLogic.Services.Interfaces.IProgramFeedbackService, DrugPreventionSystem.BusinessLogic.Services.ProgramFeedbackService>();
builder.Services.AddScoped<IProgramParticipantService, ProgramParticipantService>();
builder.Services.AddScoped<ICommunityProgramService, CommunityProgramService>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IBlogCategoryService, BlogCategoryService>();
builder.Services.AddScoped<IProgramSurveyAnswerOptionService, ProgramSurveyAnswerOptionService>();

builder.Services.AddScoped<ProgramSurveyRepository>();
builder.Services.AddScoped<ProgramSurveyService>();

builder.Services.AddScoped<IProgramSurveyQuestionService, ProgramSurveyQuestionService>();
builder.Services.AddScoped<IProgramSurveyQuestionRepository, ProgramSurveyQuestionRepository>();

builder.Services.AddScoped<IProgramSurveyService, ProgramSurveyService>();
builder.Services.AddScoped<IProgramSurveyRepository, ProgramSurveyRepository>();

builder.Services.AddSingleton<ProvideToken>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();



app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
