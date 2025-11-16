namespace FormsApp.Core.Common.Models;

public abstract record SearchRequest(int Skip, int Take = 10);