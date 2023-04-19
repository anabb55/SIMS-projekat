﻿using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
    public class ReservationDisplacementRequestService
    {
        private readonly IReservationDisplacementRequestRepository reservationDisplacementRequestRepository;

        private readonly AccommodationReservationService accommodationReservationService;
   
        public ReservationDisplacementRequestService()
        {
            reservationDisplacementRequestRepository = Inject.CreateInstance<IReservationDisplacementRequestRepository>();
      
            accommodationReservationService = new AccommodationReservationService();
        }

        public ReservationDisplacementRequest Save(ReservationDisplacementRequest request)
        {
           return reservationDisplacementRequestRepository.Save(request);   
        }

        public List<ReservationDisplacementRequest> GetAll()
        {
            List<ReservationDisplacementRequest> requests = reservationDisplacementRequestRepository.GetAll();
            if(requests.Count > 0)
			{
                BindData(requests);
            }
            
            return requests;
        }

        public void BindData(List<ReservationDisplacementRequest> requests)
        {

            foreach (ReservationDisplacementRequest r in requests)
            {
               r.Reservation = accommodationReservationService.GetById(r.ReservationId);
            }

        }

       public void BindPaticularData(ReservationDisplacementRequest request)
		{
            request.Reservation = accommodationReservationService.GetById(request.ReservationId);
		}

        public ReservationDisplacementRequest Update(ReservationDisplacementRequest request)
		{
            return reservationDisplacementRequestRepository.Update(request);
		}

        public List<ReservationDisplacementRequest> GetByOwnerId(int ownerId)
		{
            List<ReservationDisplacementRequest> requests = new List<ReservationDisplacementRequest>();
            List<ReservationDisplacementRequest> allRequests = reservationDisplacementRequestRepository.GetAll();
            BindData(allRequests);

            foreach(ReservationDisplacementRequest r in allRequests)
			{
				if (r.Reservation.Accommodation.IdUser == ownerId)
				{
                    requests.Add(r);
				}
			}
            return requests;
		}


       

        public List<ReservationDisplacementRequest> GetByUser(User user)
        {
            return reservationDisplacementRequestRepository.GetByUser(user);
        }
    }
}
