using SNSRi.Entities;
using SNSRi.Repository;
using System;
using System.Collections.Generic;

namespace SNSRi.Business
{
    public class RoomsBL : IRoomsBL
    {
        private IRoomRepository _roomRepository;
        public RoomsBL(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public IEnumerable<UIRoom> GetAllRooms()
        {
            return _roomRepository.GetAll();
        }
    }
}
