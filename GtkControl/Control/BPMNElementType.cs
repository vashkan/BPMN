using System;

namespace GtkControl.Control
{
	/// <summary>
	/// Тип элемента
	/// </summary>
	public enum BPMNElementType
	{
		/// <summary>
		/// Неопределеннный
		/// </summary>
		NONE,
		/// <summary>
		/// Начало процесса.
		/// </summary>
		/// 
		START_NONE,
		/// <summary>
		/// Конец процесса.
		/// </summary>
		END_NONE,
		/// <summary>
		/// Задача
		/// </summary>
		TASK,
		/// <summary>
		/// Безусловный поток операций
		/// </summary>
		SEQUENCE_FLOW_UNCONDITIONAL,
		/// <summary>
		/// Условный поток операций
		/// </summary>
		SEQUENCE_FLOW_CONDITIONAL,
		/// <summary>
		/// Поток операций по умолчанию.
		/// </summary>
		SEQUENCE_FLOW_DEFAULT,
		/// <summary>
		/// Промежуточное событие
		/// </summary>
		INTERMEDIATE_NONE,
		/// <summary>
		/// Ассоциация
		/// </summary>
		ASSOCIATION,
		/// <summary>
		/// Поток сообщений
		/// </summary>
		MESSAGE_FLOW,
		/// <summary>
		/// Шлюз
		/// </summary>
		GATEWAY,
		/// <summary>
		/// Пул
		/// </summary>
		POOL,
		/// <summary>
		/// Дорожка
		/// </summary>
		LANE
	};
}