using MediatR;

namespace VoidWalker.Engine.Core.Interfaces.Events;

public interface IVoidWalkerEvent : INotification, IRequest;
