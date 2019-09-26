import React from 'react';
import EventPost from '../EventPost/EventPost';
import SearchWidget from '../Widgets/SearchWidget';
import CategoriesWidget from '../Widgets/CategoriesWidget';
import { getPreviewEventList } from '../../api/EventApi'
import '../EventsView/EventsView.css';

class EventsView extends React.Component {


    constructor(props) {
        super(props);

        this.state = {
            events: [],
            query:"" 
        };

        this.moreEvents = this.moreEvents.bind(this);
        this.displayEvents = this.displayEvents.bind(this);

    }
    async componentDidMount() {
        const eventList = await getPreviewEventList(0, null, this.state.query);
        if (eventList !== undefined && eventList.events.length > 0)
            this.setState({ events: eventList.events });
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

        if (eventList !== undefined && eventList.events.length > 0)
            this.setState({
                events: eventList.events,
                category: null
            });
        else {
            this.setState({
                events: [],
                category: null
            });
        }
    }
    handleSearchCategory = async (e) => {
        e.preventDefault();
        console.log(e);
        console.log(e.target);
        
        const eventList = await getPreviewEventList(0, e.target.name==="Wszystkie"?null:e.target.name , "");

        if (eventList !== undefined && eventList.events.length > 0)
            this.setState({
                events: eventList.events,
                category: null
            });
        else {
            this.setState({
                events: [],
                category: null
            });
        }
    }
    moreEvents(event) {
        event.preventDefault();
        //
    }
    displayEvents() {
        let items=[];
        this.state.events.map((element, index) => {
            let jsDate = new Date(Date.parse(element.creationDate));
            let jsDateFormatted = jsDate.getUTCDate() + "-" + (jsDate.getMonth() + 1) + "-" + jsDate.getFullYear();
            items.push(
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
                />);
        });
        return items;
    }
    render() {
        return (
            <div>

                <div className="container">

                    <div className="row">

                        <div className="col-md-8">

                            <h1 className="my-4 text-center">Wydarzenia
                        <small> Ostatnio dodane</small>
                            </h1>

                            {this.displayEvents()}

                            <ul className="pagination justify-content-center mb-4">
                                <li className="page-item">
                                    <a className="page-link " onClick={this.moreEvents} href="#">&darr; Więcej</a>
                                </li>
                            </ul>
                        </div>
                        <div className="col-md-3 fixed" >
                            <div className="affix">
                                <SearchWidget query={this.state.query} handleSearch={this.handleSearchBar} handleOnChange={this.handleOnChange} />
                                <CategoriesWidget category={this.state.category} handleOnChange={this.handleOnChange} handleSearch={this.handleSearchCategory} />
                            </div>

                        </div>


                    </div>


                </div>

                </div>
               

        );
    }
}
export default EventsView;
