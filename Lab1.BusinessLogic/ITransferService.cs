using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1.BusinessLogic
{

    /// <summary>
    /// Интерфейс для сервиса выполнения переводов между банковскими счетами.
    /// </summary>
    internal interface ITransferService
    {
        /// <summary>
        /// Выполняет перевод средств между счетами.
        /// </summary>
        /// <param name="senderId">Идентификатор счёта-отправителя.</param>
        /// <param name="targetNumber">Номер счёта-получателя.</param>
        /// <param name="amount">Сумма перевода.</param>
        /// <returns>Результат операции перевода средств.</returns>
        OperationResult Transfer(int senderId, string? targetNumber, decimal amount);
    }
}
