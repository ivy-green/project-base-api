using ProjectBase.Application.UnitOfWork;
using Microsoft.AspNetCore.Http;
using ProjectBase.Insfracstructure.Services.FileService;
using ProjectBase.Domain.Configuration;
using ProjectBase.Domain.Pagination;
using ProjectBase.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using ProjectBase.Insfracstructure.Services.Message.SQS;
using ProjectBase.Insfracstructure.DTOs.Message;
using ProjectBase.Domain.Constants;
using Newtonsoft.Json;

namespace ProjectBase.Application.Services.UserService
{
    public class UserService : IUserService
    {
        IUnitOfWork _unitOfWork;
        IFileService _fileService;
        AppSettingConfiguration _setting;
        ISqsMessage _sqsMessage;

        public UserService(IUnitOfWork unitOfWork, 
            IFileService fileService, 
            AppSettingConfiguration setting,
            ISqsMessage sqsMessage)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _setting = setting;
            _sqsMessage = sqsMessage;
        }
        public async Task<PageList<User>> GetPagedList(int pageIndex, int pageSize)
        {
            // get list
            var users = await _unitOfWork.UserRepository.GetAll(pageIndex, pageSize);
            return users;
        }
        public async Task<List<User>> GetUserByRole(int pageIndex, int pageSize, int roleID)
        {
            // get list
            var users = await _unitOfWork.UserRepository
                                        .GetListByCondition(
                                                user => !user.UserRoles.Where(x => x.Role.Id == roleID).IsNullOrEmpty(),
                                                pageIndex, pageSize);

            return users;
        }

        public async Task UploadProfileImage(IFormFile file, string mail)
        {
            // upload file
            await _fileService.UploadFileS3(_setting.AWSSection.UserFileBucket, file);

            // data for message
            var data = new UploadProfileMessage
            {
                Mail = mail
            };

            var content = new MessageDTO
            {
                Type = MessageType.UPLOAD_PROFILE_TYPE,
                Content = JsonConvert.SerializeObject(data)
            };

            // send sns
            await _sqsMessage.SendSqsMessage(_setting.AWSSection.SQSUrl1, content);
        }
    }
}
