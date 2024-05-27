using System;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ESGCsvUploader;

namespace ESG_API.Controllers
{
    [Route("api/[controller]")]
    public class BaseController<TModel, TController> : ControllerBase
    {
        private readonly IRepository<TModel, int> _repository;
        private readonly IValidator<TModel> _validator;
        protected readonly ILogger<TController> Logger;
        public BaseController(IRepository<TModel, int> repository, IValidator<TModel> validator, ILogger<TController> logger)
        {
            _repository = repository;
            _validator = validator;
            Logger = logger;
        }
        public List<web_api_Test> GetList(string country)
        {
            string procedureName = "[dbo].[FindPeople]";
            var result = new List<web_api_Test>();
            using (SqlCommand command = new SqlCommand(procedureName, connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Country", country));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader[0].ToString());
                        string name = reader[1].ToString();
                        float? age = float.Parse(reader[2]?.ToString());
                        string Country = reader[3].ToString();
                        float? savings = float.Parse(reader[4]?.ToString());
                        web_api_Test tmpRecord = new web_api_Test()
                        {
                            Id = id,
                            Name = Name,
                            Age = age,
                            Savings = savings
                        };
                        result.Add(tmpRecord);
                    }
                }
            }
            return result;
        }
        [HttpPost]
        public IActionResult Create([FromBody] TModel model)
        {
            var validationResult = _validator.IsValid(model);
            if (!validationResult.IsValid)
                return new BadRequestObjectResult(new ErrorResponse
                {
                    ErrorCode = "MODEL_INVALID",
                    Description = "The model contains invalid values",
                    AssociatedErrors = validationResult.Errors.Select(x => x.ErrorMessage).ToArray(),
                    ErrorDateTime = DateTime.Now
                });

            try
            {
                var createdObj = _repository.Create(model);

                if (createdObj == null)
                    return new BadRequestObjectResult(new ErrorResponse
                    {
                        Description = "Database Operation Failed",
                        ErrorCode = "DB_FAILED",
                        ErrorDateTime = DateTime.Now
                    });

                return new OkObjectResult(createdObj);
            }
            catch (DataException dex)
            {
                return new BadRequestObjectResult(new ErrorResponse
                {
                    Description = dex.Message,
                    ErrorCode = "DB_FAILED",
                    ErrorDateTime = DateTime.Now
                });
            }
        }
    }
}
