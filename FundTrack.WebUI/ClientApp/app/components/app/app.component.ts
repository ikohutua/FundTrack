import { Component, OnInit } from '@angular/core';
import { IOrganizationsForLayout } from "../../viewModels/Abstract/IOrganizationsForLayout";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    private orgs: IOrganizationsForLayout[];


    ngOnInit(): void {
        this.orgs = [
            {
                id: 1,
                name: 'Our Soldier'
            },
            {
                id: 2,
                name: 'Fenix wings'
            },
            {
                id: 3,
                name: 'Another organization'
            }
        ];
    }
}
