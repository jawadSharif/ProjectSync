import { Component, Injector } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalService } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from 'shared/paged-listing-component-base';
import {
  UserServiceProxy,
  ProjectDto,
  ProjectServiceProxy,
  ProjectListOutput
} from '@shared/service-proxies/service-proxies';
import { Router } from '@angular/router';

class PagedUsersRequestDto extends PagedRequestDto {
  keyword: string;
  isActive: boolean | null;
}

@Component({
  templateUrl: './project.component.html',
  animations: [appModuleAnimation()]
})
export class ProjectComponent extends PagedListingComponentBase<ProjectDto> {

  projects: ProjectListOutput[] = [];

  constructor(
    injector: Injector,
    private _userService: UserServiceProxy,
    private _modalService: BsModalService,
    private _projectService: ProjectServiceProxy,
    private route: Router
  ) {
    super(injector);
  }


  createProject(): void {
    this.route.navigate["/app/create-project"];
  }

  protected list(
    request: PagedUsersRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    this._projectService.getAll('', '', request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      ).subscribe(r => {
        this.projects = r.items;
        this.showPaging(r, pageNumber)
      });
  }

  protected delete(user: ProjectDto): void {
    //   abp.message.confirm(
    //     this.l('UserDeleteWarningMessage', user.fullName),
    //     undefined,
    //     (result: boolean) => {
    //       if (result) {
    //         this._userService.delete(user.id).subscribe(() => {
    //           abp.notify.success(this.l('SuccessfullyDeleted'));
    //           this.refresh();
    //         });
    //       }
    //     }
    //   );
  }
}
