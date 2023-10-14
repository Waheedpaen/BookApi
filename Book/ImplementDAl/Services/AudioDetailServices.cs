using EntitiesClasses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImplementDAl.Services
{
    public class AudioDetailServices : IAudioDetailServices
    {
        private readonly IUnitofWork _unitOfWork;

        public AudioDetailServices(IUnitofWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        public async Task<AudioDetail> Create(AudioDetail model)
        {
            model.Updated_At = null;
            await _unitOfWork.IAudioDetailRepository.AddAsync(model);
            await _unitOfWork.CommitAsync();
            return model;
        }

        public async Task<AudioDetail> Delete(AudioDetail model)
        {

            var audioDetail = await _unitOfWork.IAudioDetailRepository.Delete(model);
            return audioDetail;

        }

        public async Task<AudioDetail> AudioDetailAlreadyExit(string name)
        {
            return await _unitOfWork.IAudioDetailRepository. AudioDetailAlreadyExit(name);
        }

        public async Task<AudioDetail> Get(int id)
        {
            return await _unitOfWork.IAudioDetailRepository.Get(id);
        }

        public async Task<List<AudioDetail>> GetAudioDetailByAudioScholar(int Id)
        {
            return await _unitOfWork.IAudioDetailRepository.GetAudioDetailByAudioScholar(Id);
        }

        public async Task<PagedResult<AudioDetail>> SearchAndPaginateAsync(SearchAndPaginateOptions options)
        {
            Expression<Func<AudioDetail, bool>> predicate = category =>
            string.IsNullOrEmpty(options.SearchTerm) ||
            category.Name.Contains(options.SearchTerm);

            var pagedResult = await _unitOfWork.IAudioDetailRepository.SearchAndPaginateAsync(predicate, new PaginationOptions() { PageSize = options.PageSize, Page = options.Page });
            return pagedResult;
        }

        public async Task<AudioDetail> Update(AudioDetail update, AudioDetail model)
        {
            update.Name = model.Name;
            update.AudioScholarsId = model.AudioScholarsId;
            update.Updated_At = model.Updated_At;
            update.FilePathAudio = model.FilePathAudio;
            update.FileNameAudio = model.FileNameAudio; 
            update.DateRelase = model.DateRelase;
             
            await _unitOfWork.CommitAsync();
            return update;
        }

        public async Task<AudioDetail> UpdateForAudioDetail(AudioDetail update, AudioDetail model)
        {
            update.Name = model.Name;
            update.AudioScholarsId = model.AudioScholarsId;
            update.Updated_At = model.Updated_At; 
            update.DateRelase = model.DateRelase;

            await _unitOfWork.CommitAsync();
            return update;
        }
    }
}
