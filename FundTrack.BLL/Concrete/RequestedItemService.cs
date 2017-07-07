using FundTrack.BLL.Abstract;
using System;
using System.Collections.Generic;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.DAL.Abstract;
using System.Linq;
using FundTrack.DAL.Entities;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Requested item service
    /// </summary>
    public class RequestedItemService : IRequestedItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedItemService"/> class.
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public RequestedItemService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Creates requested item in database
        /// </summary>
        /// <param name="requestedItemViewModel">Requested item view model</param>
        /// <returns>Requested item view model</returns>
        public RequestedItemViewModel CreateRequestedItem(RequestedItemViewModel requestedItemViewModel)
        {
            try
            {
                RequestedItem requestedItem = requestedItemViewModel;
                RequestedItemViewModel item =  this._unitOfWork.RequestedItemRepository.Create(requestedItem);
                this._unitOfWork.SaveChanges();

                return item;
            }
            catch(Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Delete requested item from database
        /// </summary>
        /// <param name="id">Id of requested item</param>
        public void DeleteRequestedItem(int id)
        {
            try
            {
                this._unitOfWork.RequestedItemRepository.Delete(id);
                this._unitOfWork.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets all requested items from database
        /// </summary>
        /// <returns>List of requested item view model</returns>
        public List<RequestedItemViewModel> GetAllItems()
        {
            try
            {
                var allRequestedItems = this._unitOfWork.RequestedItemRepository.Read();
                return allRequestedItems.Select(r => (RequestedItemViewModel)r).ToList();
            }
            catch(Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets requested item from database
        /// </summary>
        /// <param name="id">Id of requested item</param>
        /// <returns>Requested item view model</returns>
        public RequestedItemViewModel GetItemById(int id)
        {
            try
            {
                return this._unitOfWork.RequestedItemRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Updates requested item in database
        /// </summary>
        /// <param name="requestedItemViewModel">Requested item view model</param>
        /// <returns>Requested item view model</returns>
        public RequestedItemViewModel UpdateRequestedItem(RequestedItemViewModel requestedItemViewModel)
        {
            try
            {
                RequestedItem requestedItem = requestedItemViewModel;
                RequestedItemViewModel item = this._unitOfWork.RequestedItemRepository.Update(requestedItem);
                this._unitOfWork.SaveChanges();

                return item;
               
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }
    }
}
