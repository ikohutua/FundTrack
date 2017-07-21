import { Component, Input} from "@angular/core"
import { RequestManagementViewModel } from "../../view-models/abstract/organization-management-view-models/request-management-view-model";

@Component({
    selector: 'detail-info',
    template: require('./detail-info-request.component.html'),
    styles: [require('./detail-info-request.component.css')],
})

export class DetailInfoRequestedItemComponent{
    @Input() detailInfoRequest: RequestManagementViewModel = new RequestManagementViewModel();
}