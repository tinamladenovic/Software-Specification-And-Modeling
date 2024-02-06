using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Model.Enums;
using FiveStarTours.Serializer;

namespace FiveStarTours.Model
{
    public class ReservationChange : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; }
        public DateTime NewStartDate { get; set; }
        public DateTime NewEndDate { get; set; }

        public ReservationChangeStatusType Status { get; set; }
        public bool Delivered { get; set; }
        public string Comment { get; set; }
        public bool IsBusy { get; internal set; }

        public ReservationChange(int id, AccommodationReservation accommodationReservation, DateTime newStartDate, DateTime newEndDate, ReservationChangeStatusType status, bool delivered, string comment)
        {
            Id = id;
            AccommodationReservation = accommodationReservation;
            NewStartDate = newStartDate;
            NewEndDate = newEndDate;
            Status = status;
            Delivered = delivered;
            Comment = comment;
            IsBusy = false;
        }

        public ReservationChange()
        { }
        public string[] ToCSV()
        {

            string[] csvValues = {
                Id.ToString(),
                AccommodationReservation.Id.ToString(),
                string.Join(';', NewStartDate),
                string.Join(';', NewEndDate),
                Status.ToString(),
                Delivered.ToString(),
                Comment

        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationReservation = new AccommodationReservation() { Id = Convert.ToInt32(values[1]) };
            NewStartDate = Convert.ToDateTime(values[2]);
            NewEndDate = Convert.ToDateTime(values[3]);
            Status = Enum.Parse<ReservationChangeStatusType>(values[4]);
            Delivered = Convert.ToBoolean(values[5]);
            Comment = values[6];


        }
    }
}
