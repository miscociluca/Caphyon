
var taskInput = document.getElementById("new-task");//Add a new task.
var addButton = document.getElementsByTagName("button")[0];//first button
var incompleteTaskHolder = document.getElementById("incomplete-tasks");//ul of #incomplete-tasks
var completedTasksHolder = document.getElementById("completed-tasks");//completed-tasks

$(document).ready(function () {
	addButton.onclick = addTask;
	ajaxRequestGetAllTasks();
	$(".more").click(function () {
		$(".dropdown-list").css("display") === "none" ? $(".dropdown-list").css("display", "block") : $(".dropdown-list").css("display", "none");
	});
	$("#ToDoButt").click(function () {
		$("#todo").css("display") === "none" ? $("#todo").css("display", "block") : $("#todo").css("display", "none");
	});
	$(document.body).on('change', '#SelectOption', function () {

		var option = $("#SelectOption option:selected").text();
		if (option === "Completed") {
			$("#Todo").css("display", "none");

			$("#completed").css("display", "block");
		}
		else {
			$("#completed").css("display", "none");
			$("#Todo").css("display", "block");
		}
	});

	$("#dropdown-list li").click(function () {

		var listitem = this.textContent;
		if (listitem === "Mark all tasks as completed") {
			console.log("Complete ALL Tasks...");
			if (incompleteTaskHolder.childNodes.length > 0) {
				//Append the task list items to the #completed-tasks
				for (var item = 1; i <= incompleteTaskHolder.childNodes.length; i++) {
					var listItem = incompleteTaskHolder.childNodes[item];
					completedTasksHolder.appendChild(listItem);
					bindTaskEvents(listItem, taskIncomplete);
				}
				ajaxRequestCompleteALLTasks();
			}
		}
		else
		{
			completedTasksHolder.innerHTML="";
			incompleteTaskHolder.innerHTML = "";
			ajaxRequestDeleteALLTasks();
		}
	});
});

//New task list item
var createNewTaskElement = function (taskString, id) {

	var listItem = document.createElement("li");
	if (id !== null) {
		listItem.id = id;
	}
	//input (checkbox)
	var checkBox = document.createElement("input");//checkbx
	//label
	var label = document.createElement("label");//label
	//input (text)
	var editInput = document.createElement("input");//text
	//button.edit
	var editButton = document.createElement("button");//edit button

	//button.delete
	var deleteButton = document.createElement("button");//delete button

	label.innerText = taskString;

	//Each elements, needs appending
	checkBox.type = "checkbox";
	editInput.type = "text";

	editButton.innerText = "Edit";
	editButton.className = "edit";
	deleteButton.innerText = "Delete";
	deleteButton.className = "delete";
	//and appending.
	listItem.appendChild(checkBox);
	listItem.appendChild(label);
	listItem.appendChild(editInput);
	listItem.appendChild(editButton);
	listItem.appendChild(deleteButton);
	return listItem;
};
var addTask = function () {
	console.log("Add Task...");
	//Create a new list item with the text from the #new-task:
	var listItem = createNewTaskElement(taskInput.value, null);
	//Append listItem to incompleteTaskHolder
	incompleteTaskHolder.appendChild(listItem);
	bindTaskEvents(listItem, taskCompleted);
	ajaxRequestADDTask(taskInput.value);
	taskInput.value = "";
};

var editTask = function () {
	console.log("Edit Task...");
	var listItem = this.parentNode;
	var editInput = listItem.querySelector('input[type=text]');
	var label = listItem.querySelector("label");
	var containsClass = listItem.classList.contains("editMode");
	//If class of the parent is .editmode
	if (containsClass) {
		//switch to .editmode
		//label becomes the inputs value.
		label.innerText = editInput.value;
		ajaxRequestEditTask(listItem.id, label.innerText);
	} else {
		editInput.value = label.innerText;
	}
	//toggle .editmode on the parent.
	listItem.classList.toggle("editMode");
};

//Delete task.
var deleteTask = function () {
	console.log("Delete Task...");
	var listItem = this.parentNode;
	var ul = listItem.parentNode;
	//Remove the parent list item from the ul.
	ul.removeChild(listItem);
	ajaxRequestDeleteTask(listItem.id);
};

//Mark task completed
var taskCompleted = function () {
	console.log("Complete Task...");
	//Append the task list item to the #completed-tasks
	var listItem = this.parentNode;
	completedTasksHolder.appendChild(listItem);
	bindTaskEvents(listItem, taskIncomplete);
	ajaxRequestCompleteTask(listItem.id);
};

//Mark task incompleted
var taskIncomplete = function () {
	console.log("Incomplete Task...");
	var listItem = this.parentNode;
	incompleteTaskHolder.appendChild(listItem);
	bindTaskEvents(listItem, taskCompleted);
	ajaxRequestIncompleteTask(listItem.id);
};



var ajaxRequestGetAllTasks = function () {
	$.ajax({
		url: 'https://localhost:44356/api/GetTasks',
		method: 'GET',
		headers: {
		},
		success: function (data) {
			var incomplete_tasks_list = $("#incomplete-tasks");
			var complete_tasks_list = $("#completed-tasks");
			$.each(data, function (index, value) {
				if (value.Status === true) {
					//var row1 = $("<li value=" + value.Id + "><input type='checkbox' checked><label>" + value.Descriere+"</label><input type='text'><button class='edit'>Edit</button><button class='delete'>Delete</button></li>");
					//complete_tasks_list.append(row1);
					var listItem = createNewTaskElement(value.Descriere, value.Id);
					completedTasksHolder.appendChild(listItem);
					bindTaskEvents(listItem, taskIncomplete);
				}
				else {
					//var row2 = $("<li value=" + value.Id + "><input type='checkbox'><label>" + value.Descriere + "</label><input type='text'><button class='edit'>Edit</button><button class='delete'>Delete</button></li>");
					//incomplete_tasks_list.append(row2);
					var listItem2 = createNewTaskElement(value.Descriere, value.Id);
					incompleteTaskHolder.appendChild(listItem2);
					bindTaskEvents(listItem2, taskCompleted);
				}
			});
		},
		error: function (data) {
		}
	});
}
var ajaxRequestDeleteTask = function (id) {
	$.ajax({
		url: 'https://localhost:44356/api/DeleteTask/' + id,
		method: 'Delete',
		headers: {
		},
		success: function (data) {
			console.log("task with id" + id + " has been deleted");
		},
		error: function (data) {
			console.log("task with id" + id + " has NOT been deleted.AN ERROR OCCURED!!!");
		}
	});
}
var ajaxRequestADDTask = function (description) {
	$.ajax({
		url: 'https://localhost:44356/api/AddTask/',
		method: 'Post',
		headers: {
		},
		data: {
			Descriere: description,
			Status: false
		},
		success: function (data) {
			console.log("task with id " + data + " has been added");
		},
		error: function (data) {
			console.log("AN ERROR OCCURED!!!");
		}
	});
};

var ajaxRequestEditTask = function (id,taskdesc) {
	$.ajax({
		url: 'https://localhost:44356/api/UpdateTask/'+id,
		method: 'Put',
		headers: {
		},
		data: {
			Id:id,
			Descriere: taskdesc
		},
		success: function (data) {
			console.log("task with id " + data + " has been updated");
		},
		error: function (data) {
			console.log("AN ERROR OCCURED!!!");
		}
	});
}
var ajaxRequestCompleteTask = function (id) {
	$.ajax({
		url: 'https://localhost:44356/api/CompleteTask/' + id,
		method: 'Patch',
		headers: {
		},
		success: function (data) {
			console.log("task with id " + data + " has been completed");
		},
		error: function (data) {
			console.log("AN ERROR OCCURED!!!");
		}
	});
};
var ajaxRequestIncompleteTask = function (id) {
	$.ajax({
		url: 'https://localhost:44356/api/IncompleteTask/' + id,
		method: 'Patch',
		headers: {
		},
		success: function (data) {
			console.log("task with id " + data + " must be done");
		},
		error: function (data) {
			console.log("AN ERROR OCCURED!!!");
		}
	});
};
var ajaxRequestDeleteALLTasks = function () {
	$.ajax({
		url: 'https://localhost:44356/api/DeleteAll/',
		method: 'Post',
		headers: {
		},
		success: function (data) {
			console.log("All tasks have been deleted");
		},
		error: function (data) {
			console.log("AN ERROR OCCURED!!!");
		}
	});
};
var ajaxRequestCompleteALLTasks = function () {
	$.ajax({
		url: 'https://localhost:44356/api/CompleteAll/',
		method: 'Post',
		headers: {
		},
		success: function (data) {
			console.log("All tasks have been completed");
		},
		error: function (data) {
			console.log("AN ERROR OCCURED!!!");
		}
	});
};
var bindTaskEvents = function (taskListItem, checkBoxEventHandler) {
	console.log("bind list item events");
	//select ListItems children
	var checkBox = taskListItem.querySelector("input[type=checkbox]");
	var editButton = taskListItem.querySelector("button.edit");
	var deleteButton = taskListItem.querySelector("button.delete");

	//Bind editTask to edit button.
	editButton.onclick = editTask;
	//Bind deleteTask to delete button.
	deleteButton.onclick = deleteTask;
	//Bind taskCompleted to checkBoxEventHandler.
	checkBox.onchange = checkBoxEventHandler;
}

//cycle over incompleteTaskHolder ul list items
for (var i = 0; i < incompleteTaskHolder.children.length; i++) {

	//bind events to list items chldren(tasksCompleted)
	bindTaskEvents(incompleteTaskHolder.children[i], taskCompleted);
}
//cycle over completedTasksHolder ul list items
for (var j = 0; j < completedTasksHolder.children.length; j++) {
	//bind events to list items chldren(tasksIncompleted)
	bindTaskEvents(completedTasksHolder.children[j], taskIncomplete);
}

