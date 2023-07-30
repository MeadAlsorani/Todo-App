import { Pipe, PipeTransform } from '@angular/core';
import { TodoTagDto } from '../web-api-client';

@Pipe({
  name: 'tagsBinder',
})
export class TagsBinderPipe implements PipeTransform {
  transform(id: number, tags: TodoTagDto[]): any {
    const tag = tags.find((x) => x.id === id);
    return tag?.name ?? '';
  }
}
