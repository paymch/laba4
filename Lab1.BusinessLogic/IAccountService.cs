using Lab1.Model.ModelsDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1.BusinessLogic
{

    /// <summary>
    /// Интерфейс для сервиса управления банковскими счетами.
    /// </summary>
    internal interface IAccountService
    {
        /// <summary>
        /// Получает список банковских счетов.
        /// </summary>
        /// <param name="deleted">Флаг, указывающий, нужно ли включать удалённые счета.</param>
        /// <returns>Список банковских счетов.</returns>
        List<BankAccount> GetAccounts(bool deleted);

        /// <summary>
        /// Получает список активных банковских счетов.
        /// </summary>
        /// <returns>Список активных банковских счетов.</returns>
        List<BankAccount> GetActiveAccounts();
        /// <summary>
        /// Получает банковский счёт по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор счёта.</param>
        /// <returns>Банковский счёт или null, если не найден.</returns>
        BankAccount? GetById(int id);
        /// <summary>
        /// Получает банковский счёт по его номеру.
        /// </summary>
        /// <param name="number">Номер счёта.</param>
        /// <param name="includeDeleted">Флаг, указывающий, нужно ли включать удалённые счета.</param>
        /// <returns>Банковский счёт или null, если не найден.</returns>
        BankAccount? FindByNumber(string? number, bool includeDeleted = false);
        /// <summary>
        /// Создаёт новый банковский счёт.
        /// 
        /// </summary>
        /// <param name="number">Номер счёта.</param>
        /// <param name="ownerId">Идентификатор владельца счёта.</param>
        /// <param name="typeId">Идентификатор типа счёта.</param>
        /// <param name="initialBalance">Начальный баланс счёта.</param>
        /// <returns>Результат операции создания счёта.</returns>
        OperationResult AddAccount(string? number, int ownerId, int typeId, decimal initialBalance);
        /// <summary>
        /// Изменяет владельца банковского счёта.
        /// </summary>
        /// <param name="accountId">Идентификатор счёта.</param>
        /// <param name="newOwnerId">Идентификатор нового владельца.</param>
        /// <returns>Результат операции изменения владельца.</returns>
        OperationResult ChangeOwner(int accountId, int newOwnerId);
        /// <summary>
        /// Удаляет банковский счёт.
        /// </summary>
        /// <param name="accountId">Идентификатор счёта.</param>
        /// <returns>Результат операции удаления счёта.</returns>
        OperationResult DeleteAccount(int accountId);
        /// <summary>
        /// Восстанавливает удалённый банковский счёт.
        /// </summary>
        /// <param name="accountId">Идентификатор счёта.</param>
        /// <returns>Результат операции восстановления счёта.</returns>
        OperationResult RestoreAccount(int accountId);
        OperationResult ToggleFreeze(int accountId);
    }
}
