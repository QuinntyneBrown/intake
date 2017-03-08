import { Project } from "./project.model";
import { ProjectService } from "./project.service";

const template = require("./project-list.component.html");
const styles = require("./project-list.component.scss");

export class ProjectListComponent extends HTMLElement {
    constructor(
		private _document: Document = document,
		private _projectService: ProjectService = ProjectService.Instance) {
        super();
    }

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
		this._bind();
    }

	private async _bind() {
		const projects: Array<Project> = await this._projectService.get();
        for (var i = 0; i < projects.length; i++) {
			let el = this._document.createElement(`ce-project-item`);
			el.setAttribute("entity", JSON.stringify(projects[i]));
			this.appendChild(el);
        }	
	}
}

customElements.define("ce-project-list", ProjectListComponent);
