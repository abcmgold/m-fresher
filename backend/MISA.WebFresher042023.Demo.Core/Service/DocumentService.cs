using AutoMapper;
using Microsoft.AspNetCore.Http.Features;
using MISA.WebFresher042023.Demo.Core.Dto.Document;
using MISA.WebFresher042023.Demo.Core.Dto.Hanover;
using MISA.WebFresher042023.Demo.Core.Dto.PropertyTransfer;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.Enum;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Interface.Service;
using MISA.WebFresher042023.Demo.Infrastructure.Interface;

namespace MISA.WebFresher042023.Demo.Core.Service
{
    public class DocumentService : BaseService<Document, DocumentDto, DocumentUpdateDto, DocumentCreateDto>, IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IPropertyTransferRepository _propertyTransferRepository;
        public DocumentService(IDocumentRepository documentRepository, IMapper mapper, IUnitOfWork unitOfWork, IPropertyTransferRepository propertyTransferRepository) : base(documentRepository, mapper, unitOfWork)
        {
            _documentRepository = documentRepository;
            _propertyTransferRepository = propertyTransferRepository;
        }

        public async Task<object> GetByPaging(int pageNumber, int pageSize)
        {
            var result = await _documentRepository.GetByPaging(pageNumber, pageSize);

            return result;
        }

        public async Task<int> AddDocumentAsync(DocumentCreateDto documentCreateDto)
        {

            Document document = _mapper.Map<Document>(documentCreateDto);
            List<Document> documentList = new List<Document>();
            documentList.Add(document);
            List<PropertyTransfer> propertyTransfer = _mapper.Map<List<PropertyTransfer>>(documentCreateDto.PropertyTransferCreateList);

            List<Handover> handovers = _mapper.Map<List<Handover>>(documentCreateDto.HandoverCreateList);
            Guid idDocument = document.DocumentId; // Lấy id của document[0]

            foreach (var pt in propertyTransfer)
            {
                pt.DocumentId = idDocument; // Gán giá trị idDocument trong PropertyTransfer
            }

            foreach (var handover in handovers)
            {
                handover.DocumentId = idDocument; // Gán giá trị idDocument trong PropertyTransfer
            }

            try
            {
                _unitOfWork.BeginTransaction();
                await _documentRepository.InsertAsync(documentList);
                await _propertyTransferRepository.InsertAsync(propertyTransfer);
                _unitOfWork.Commit();

                return 1;

            }
            catch (Exception)
            {
                 _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<int> UpdateDocumentAsync(DocumentUpdateDto documentUpdateDto)
        {
            List<PropertyTransferUpdateDto> propertyTransferUpdatesChange = new List<PropertyTransferUpdateDto>();
            List<PropertyTransferUpdateDto> propertyTransferUpdatesDelete = new List<PropertyTransferUpdateDto>();
            List<PropertyTransferUpdateDto> propertyTransferUpdatesNoChange = new List<PropertyTransferUpdateDto>();
            List<PropertyTransferUpdateDto> propertyTransferUpdatesInsert = new List<PropertyTransferUpdateDto>();

            foreach (var property in documentUpdateDto.PropertyTransferUpdateList)
            {
                switch (property.StatusRerord)
                {
                    case StatusRecord.Update:
                        propertyTransferUpdatesChange.Add(property);
                        break;

                    case StatusRecord.Insert:
                        propertyTransferUpdatesInsert.Add(property);
                        break;

                    case StatusRecord.Delete:
                        propertyTransferUpdatesDelete.Add(property);
                        break;
                    default:
                        break;
                }
            }

            Document document = _mapper.Map<Document>(documentUpdateDto);
            List<Document> documentList = new List<Document>();
            documentList.Add(document);

            List<PropertyTransfer> propertyTransferChange = _mapper.Map<List<PropertyTransfer>>(propertyTransferUpdatesChange);
            List<PropertyTransfer> propertyTransferInsert = _mapper.Map<List<PropertyTransfer>>(propertyTransferUpdatesInsert);
            List<Handover> handovers = _mapper.Map<List<Handover>>(documentUpdateDto.HandoverUpdateList);

            try
            {
                _unitOfWork.BeginTransaction();
                await _documentRepository.UpdateAsync(documentList);
                await _propertyTransferRepository.UpdateAsync(propertyTransferChange);
                await _propertyTransferRepository.InsertAsync(entity: propertyTransferInsert);
                await _propertyTransferRepository.DeleteAsync(propertyTransferUpdatesDelete);

                _unitOfWork.Commit();

                return 1;

            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }
    }
}
