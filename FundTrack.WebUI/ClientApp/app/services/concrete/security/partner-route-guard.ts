import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { BaseGuardService } from '../../abstract/base-guard-service';

@Injectable()
export class PartnerRouteGuard extends BaseGuardService {
    constructor(_router: Router) {
        super(_router,null);
    }
}
