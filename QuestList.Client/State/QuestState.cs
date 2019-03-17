using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using QuestList.Shared.Models;

namespace QuestList.Client.State
{
    public class QuestState
    {
        private readonly HttpClient _http;

        public event EventHandler OnStateChanged;
        public IList<QuestLine> Quests { get; set; } = new List<QuestLine>();
        public QuestLine CurrentQuest;
        public QuestTask CurrentTask { get; set; }

        public QuestState(HttpClient http)
        {
            _http = http;

            _ = PopulateQuests();
        }

        public bool IsCurrentQuest(QuestLine quest)
        {
            return quest == CurrentQuest;
        }

        public async Task PopulateQuests()
        {
            Quests = await _http.GetJsonAsync<IList<QuestLine>>("/quests");
            StateHasChanged();
        }

        public async Task SelectQuest(QuestLine quest)
        {
            CurrentQuest = quest;

            if (CurrentQuest.Tasks.Count == 0)
            {
                CurrentQuest.Tasks = await _http.GetJsonAsync<IList<QuestTask>>($"/quests/{CurrentQuest.Id}/tasks");
            }

            StateHasChanged();
        }

        public async Task UpdateQuestStatus(QuestLine quest)
        {
            quest.IsCompleted = !quest.IsPermanent && quest.Tasks.All(t => t.IsCompleted);
            quest.IsBeingWorked = !quest.IsCompleted && quest.IsBeingWorked;

            await _http.PutJsonAsync($"/quests/{quest.Id}", quest);

            StateHasChanged();
        }

        public async Task ToggleQuest(QuestLine quest)
        {
            quest.IsBeingWorked = !quest.IsBeingWorked;

            try
            {
                await _http.PutJsonAsync($"/quests/{quest.Id}", quest);
            }
            catch
            {
                quest.IsBeingWorked = !quest.IsBeingWorked;
            }

            StateHasChanged();
        }

        public void SelectTask(QuestTask task)
        {
            CurrentTask = task;
        }

        public async Task ToggleTask(QuestTask task)
        {
            task.IsCompleted = !task.IsCompleted;

            try
            {
                await _http.PutJsonAsync($"/quests/{CurrentQuest.Id}/tasks/{task.Id}", task);
                await UpdateQuestStatus(CurrentQuest);
            }
            catch
            {
                task.IsCompleted = !task.IsCompleted;
            }
        }

        public bool IsCurrentTask(QuestTask task)
        {
            return task == CurrentTask;
        }

        private void StateHasChanged()
        {
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
