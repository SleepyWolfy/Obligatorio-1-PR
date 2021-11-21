using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business;
using BusinessInterfaces;
using Common.Protocol;
using Domain.BusinessObjects;
using Exceptions.BusinessExceptions;
using Grpc.Core;
using LogCommunicator;
using LogCommunicatorInterfaces;
using Microsoft.Extensions.Logging;

namespace ServerApplicationWithGrpc
{
    public class GameMessagerService : GameMessager.GameMessagerBase
    {
        private readonly ILogger<GameMessagerService> _logger;
        private readonly IGameLogic _gameLogic;
        private readonly ILogSender _logSender;
        private readonly ILogGenerator _logGenerator;

        public GameMessagerService(ILogger<GameMessagerService> logger, ILogSender logSender)
        {
            _logger = logger;
            _logSender = logSender;
            _gameLogic = new GameLogic();
            _logGenerator = new LogGenerator();
        }

        public override Task<GameReply> AddGame(AddGameRequest request, ServerCallContext context)
        {
            Game game = new Game(request.Gamename, request.Genre, request.Esrb, request.Synopsis, request.PathAFoto, -1);
            game.Owner = new User("ADMIN", 3007);
            string logMessage = "";
            int statusCode = StatusCodeConstants.OK;

            try
            {
                int id = _gameLogic.AddGame(game);
                logMessage = $"The game {game.Title} has been published by {game.Owner.Username}.";
                _logSender.SendLog(_logGenerator.CreateLog("ADMIN", id, false, logMessage));
            }
            catch(BusinessException e)
            {
                statusCode = StatusCodeConstants.ERROR_CLIENT;
                logMessage = e.Message;
            }
            catch(Exception e)
            {
                statusCode = StatusCodeConstants.ERROR_SERVER;
                logMessage = e.Message;
            }
            
            return Task.FromResult(new GameReply
            {
                StatusCode = statusCode,
                Message = logMessage
            });
        }
    }
}
