import React from 'react';
import EventPost from '../EventPost/EventPost';
import SearchWidget from '../Widgets/SearchWidget';
import CategoriesWidget from '../Widgets/CategoriesWidget';
import Shortcuts from '../Shortcuts/Shortcuts';
import { Redirect } from 'react-router';
import { getPreviewEventList } from '../../api/EventApi';
import { userIsAuthenticated } from '../../utils/JwtUtils';
import '../EventsView/EventsView.css';

class EventsView extends React.Component {


    constructor(props) {
        super(props);

        this.state = {
            events: [],
            query: "",
            visibleQuery: "",
            canLoadMore: false,
            redirect: false,
            category: "Wszystkie",
            pageNumber: 0,
            avatarPath : null
        };

        this.displayEvents = this.displayEvents.bind(this);

    }
    async componentDidMount() {
        const eventList = await getPreviewEventList(0, null, this.state.query);
        console.log(eventList)
        if (eventList !== undefined && eventList.events.length > 0)
            this.setState({
                events: eventList.events,
                canLoadMore: eventList.canLoadMore,
                avatarPath: eventList.avatarPath
            });
        console.log(eventList);


    }
    handleOnChange = (e) => {
        this.setState({
            [e.target.name]:e.target.value
        });

    }
    handleSearchBar = async (e) => {
        e.preventDefault();
        const eventList = await getPreviewEventList(0, null, this.state.query);
        this.setState({ visibleQuery: this.state.query });

        if (eventList !== undefined && eventList.events.length > 0)
            this.setState({
                events: eventList.events,
                category: "Wszystkie"
            });
        else {
            this.setState({
                events: [],
                category: "Wszystkie"
            });
        }
    }
    handleClick = (e) => {
        this.setState({
            redirect: true,
            redirectTarget: e.target.getAttribute('name')
        });
    }
    redirect = () => {
        return <Redirect to={`/konto/${this.state.redirectTarget}`} />;
    }
    handleSearchCategory = async (e) => {
        e.preventDefault();
        const name = typeof e.target.name === "undefined" ? e.target.getAttribute("name") : e.target.name;
        console.log(e.target.name)
        console.log(name);
        const eventList = await getPreviewEventList(0, e.target.name==="Wszystkie"?null:name , "");

        if (eventList !== undefined && eventList.events.length > 0)
            this.setState({
                events: eventList.events,
                category: name,
                visibleQuery: "",
                query:""
            });
        else {
            this.setState({
                events: [],
                category: name,
                visibleQuery: "",
                query:""
            });
        }
    }

    moreEvents =  async(event) => {
        event.preventDefault();

        const eventList = await getPreviewEventList(this.state.pageNumber+1, null, this.state.query);

        const items = this.state.events.concat(eventList.events);

        if (eventList !== undefined && eventList.events.length > 0)
            this.setState({
                events: items,
                pageNumber: this.state.pageNumber + 1,
                canLoadMore: eventList.canLoadMore
            });
        
    }
    displayEvents() {
        let items=[];
        this.state.events.map((element, index) => {
            let jsDate = new Date(Date.parse(element.creationDate));
            let jsDateFormatted = jsDate.getUTCDate() + "-" + (jsDate.getMonth() + 1) + "-" + jsDate.getFullYear();
            items.push(
                <div className="card mt-4">
                    <div className="card-header myCard" >
                        <EventPost
                            urlSlug={element.urlSlug}
                            isPreview={false}
                            currentReaction={element.currentReaction}
                            key={index}
                            createdBy={element.createdBy}
                            comments={element.comments}
                            canLoadMore={element.canLoadMore}
                            commentsCount={element.commentsCount}
                            eventName={element.name}
                            id={element.id}
                            date={jsDateFormatted}
                            reactions={element.reactions}
                            reactionsCount={element.reactionsCount}
                            eventDescription={element.shortContent}
                            picture={element.image}
                            createdById={element.createdById}
                            avatarPath={this.state.avatarPath}
                        />
                    </div>
                </div>
                        );
        });
        return items;
    }
    displayHeader = () => {
        if (this.state.category === "Wszystkie" && this.state.visibleQuery === "") {
            return "Ostatnio dodane"
        }
        else if (this.state.category === "Wszystkie" && this.state.visibleQuery !== "") {
            return `Wyszukane po ${this.state.visibleQuery}`
        }
        else {
            return `Z kategorii ${this.state.category}`
        }
    }
    render() {
        return (
            <div>
                {this.state.redirect === true ? this.redirect() : null}
                {this.state.imageClicked === true ? this.displayImageZoom() : null}

                <div className="container">

                    <div className="row">

                        <div className="col-md-8">
                            <h4 className=" text-muted mt-4" > {this.displayHeader()}</h4>
                            <hr className="myHr" />
                            {this.displayEvents()}
                           
                            <ul className="pagination justify-content-center mb-4">
                                <li className="page-item">
                                    {this.state.canLoadMore === true
                                        ?
                                        <button className="page-link " onClick={this.moreEvents} href="#">&darr; Więcej</button>
                                        :
                                        null}
                                </li>
                            </ul>
                        </div>
                        <div className="col-md-3 fixed mt-5" >
                            <div className="affix">
                                <div className="card">
                                    <div className="card-header">
                                        <SearchWidget    query={this.state.query} handleSearch={this.handleSearchBar} handleOnChange={this.handleOnChange} />
                                        <CategoriesWidget category={this.state.category} handleOnChange={this.handleOnChange} handleSearch={this.handleSearchCategory} />
                                        {userIsAuthenticated()
                                            ?
                                            <Shortcuts handleClick={this.handleClick} />
                                            :
                                            null
                                            }
                                    </div>
                                </div>
                             
                            </div>

                        </div>


                    </div>


                </div>

                </div>
               

        );
    }
}
export default EventsView;
