import { ComponentFixture, TestBed } from '@angular/core/testing';
import { TodoComponent } from './todo.component';
import { FormBuilder } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap/modal';
import {
  TodoListsClient,
  TodoItemsClient,
  UpdateTodoItemDetailCommand,
  TodoItemDto,
  // Import other dependencies or create mock services as needed
} from '../web-api-client';
import { of } from 'rxjs';

describe('TodoComponent', () => {
  let component: TodoComponent;
  let fixture: ComponentFixture<TodoComponent>;
  let todoListsClientMock: Partial<TodoListsClient>;
  let todoItemsClientMock: Partial<TodoItemsClient>;
  // Create other mocks for services used in the TodoComponent

  beforeEach(async () => {
    todoListsClientMock = {
      // Mock the required methods used in the TodoComponent
    };
    todoItemsClientMock = {
      updateItemDetails: jasmine.createSpy().and.returnValue(of({})), // Mock the updateItemDetails method
      // Mock other methods used in the TodoComponent
    };
    // Create other mock instances for services used in the TodoComponent

    await TestBed.configureTestingModule({
      declarations: [TodoComponent],
      providers: [
        FormBuilder,
        BsModalService,
        {
          provide: TodoListsClient,
          useValue: todoListsClientMock,
        },
        {
          provide: TodoItemsClient,
          useValue: todoItemsClientMock,
        },
        // Provide other mock services as needed
      ],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TodoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  // Other tests ...

  it('should update item details', () => {
    // Arrange
    const item = {
      id: 1,
      listId: 1,
      priority: 2,
      note: 'Note',
      colour: '#FFFFFF',
    } as TodoItemDto;
    component.selectedList = {
      id: 1,
      title: 'List 1',
      items: [item],
    } as any;
    component.itemDetailsFormGroup.setValue(item);

    const updatedItem = {
      ...item,
      listId: 2, // Move item to another list
      priority: 2, // Update priority
      note: 'Updated note', // Update note
      colour: 'Blue', // Update colour
    };

    // Mock the itemsClient.updateItemDetails method to return the updatedItem
    spyOn(todoItemsClientMock, 'updateItemDetails').and.returnValue(
      of(updatedItem as any)
    );
    // Act
    component.updateItemDetails();

    // Assert
    // Verify that the itemsClient.updateItemDetails method is called with the correct arguments
    expect(todoItemsClientMock.updateItemDetails).toHaveBeenCalledWith(
      item.id,
      jasmine.any(UpdateTodoItemDetailCommand)
    );

    // Verify that the selectedList's items have been updated with the updatedItem
    expect(component.selectedList.items).not.toContain(item);
    expect(component.selectedList.items).toContain(updatedItem as any);

    // Verify that the itemDetailsModalRef and itemDetailsFormGroup are reset
    expect(component.itemDetailsModalRef.hide).toHaveBeenCalled();
    expect(component.itemDetailsFormGroup.value).toEqual({
      id: null,
      listId: null,
      priority: '',
      note: '',
      colour: '',
    });
  });
});
