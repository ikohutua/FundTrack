import { Component, OnInit } from '@angular/core';
import { MainPageViewModel, IMainPageViewModel } from "./mainPageViewModel";

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
    model: IMainPageViewModel;

    ngOnInit(): void {
        let DATE = new Date();
        this.model = new MainPageViewModel();
        this.model.reports = [{
            id: 1,
            organizationName: 'Our Soldier',
            organizationId: 1,
            date: <Date>DATE
        },
        {
            id: 2,
            organizationName: 'Fenix wings',
            organizationId: 2,
            date: <Date>DATE
        },
        {
            id: 2,
            organizationName: 'Another organization',
            organizationId: 3,
            date: <Date>DATE
        }];

        this.model.events = [{
            id: 1,
            description: ' Висловлюємо подяку від піхоти 30-ї бригади всім тим, хто долучився до придбання запчастини для їхнього медичного бусу - набір тросів. Піхотні підрозділи перебувають на самому "нулі", і, на жаль, не уникають поранень. Цей бус слугуватиме саме транспортуванню поранених до найближчого шпиталю.Але виявилось, що є ще один дефект, а саме, з головкою блоку циліндрів.Його вартість складає 9000 грн.Ми хотіли б допомогти з остаточним ремонтом цього бусу.Але ще хотіли нагадати, що наразі збираємо кошти на коробку передач на медичний УАЗ для піхоти вже 92- ї бригади. Тобто, загальна сума запчастин складає наразі 25 000 грн., з яких у наявності є 5000. Просимо підтримати наших військових у цій допомозі.',
            organizationId: 1,
            organizationName: 'Our Soldier',
            date: DATE,
            pathToCoverImage: 'https://scontent-waw1-1.xx.fbcdn.net/v/t1.0-9/18740007_698639290344884_2001069689308999914_n.jpg?oh=a2e3b70863f5344bfa3b49917b526920&oe=59EA3968'
        }]
    }
}
