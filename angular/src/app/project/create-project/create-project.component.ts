import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import { Component, Injector } from '@angular/core';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  AsanaServiceProxy,
  StringStringKeyValuePair,
  DevOpsServiceProxy,
  ProjectServiceProxy,
  CreateProjectDto,
  ProjectDto
} from '@shared/service-proxies/service-proxies';
import { Router } from '@angular/router';

@Component({
  templateUrl: './create-project.component.html',
  animations: [appModuleAnimation()]
})
export class CreateProjectComponent extends AppComponentBase {


  asanaWorkspaces: StringStringKeyValuePair[] = [];
  asanaProjects: StringStringKeyValuePair[] = [];
  devopsProjects: StringStringKeyValuePair[];

  isSaving = false;

  projectDto = new ProjectDto()

  constructor(
    injector: Injector,
    private _asanaService: AsanaServiceProxy,
    private _devOpsService: DevOpsServiceProxy,
    private _projectService: ProjectServiceProxy,
    private _router: Router
  ) {
    super(injector);
  }

  refreshAsanaWorkspaces() {
    this._asanaService.getAsanaWorkSpaces(this.projectDto.asanaToken).subscribe(result => {
      this.asanaWorkspaces = result;
    })
  }

  refrehAsanaTasks() {
    if (!this.projectDto.asanaToken)
      return;
    this._asanaService.getAllProjectInWorkSpace(this.projectDto.asanaWorkSpaceId, this.projectDto.asanaToken).subscribe(result => {
      this.asanaProjects = result;
    })
  }

  async refreshDevOpsProjects() {
    let accessToken = await this._devOpsService.accessToken(this.projectDto.devOpsToken).toPromise();
    this.projectDto.devOpsToken = accessToken.item1;
    this._devOpsService.getProjects(this.projectDto.devOpsOrganization, accessToken.item1, '6.0').subscribe(result => {
      this.devopsProjects = result;
    })
  }

  save() {
    this.isSaving = true;

    this.projectDto.asanaProject = this.asanaProjects.find(a => a.key == this.projectDto.asanaProjectId).value;
    this.projectDto.asanaWorkspace = this.asanaWorkspaces.find(a => a.key == this.projectDto.asanaWorkSpaceId).value;
    this.projectDto.devOpsProjectTitle = this.devopsProjects.find(a => a.key == this.projectDto.devOpsProjectId).value;

    this._projectService.createOrEditProject(this.projectDto).pipe(
      finalize(() => {
        this.isSaving = false;
      })
    ).subscribe(result => {
      abp.notify.info('saved successfully');
      this._router.navigate["/app/projects"];
    })
  }

}
