import { PipeTransform, Pipe } from "@angular/core";
import { IEventModel } from '../../view-models/abstract/event-model.interface';

@Pipe({
    name: 'absolutevalue'
})
export class AbsoluteValuePipe implements PipeTransform {
    transform(value: number): number {
        return Math.abs(value);
    };
}
