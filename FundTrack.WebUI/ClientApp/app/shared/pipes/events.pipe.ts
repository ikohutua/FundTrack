import { PipeTransform, Pipe } from "@angular/core";
import { IEventModel } from '../../view-models/abstract/event-model.interface';

@Pipe({
    name: 'eventFilter'
})
export class EventFilterPipe implements PipeTransform {
    transform(value: IEventModel[], filterBy: Number): IEventModel[] {
        filterBy = filterBy ? filterBy : null;
        return filterBy ? value.filter((event: IEventModel) =>
            event.organizationId == filterBy) : value;
    };
}