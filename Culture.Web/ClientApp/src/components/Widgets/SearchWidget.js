
import React from 'react';


class SearchWidget extends React.Component {

    constructor(props) {
        super(props);
    }
    render() {
        return (


            <div className="card my-4">
                <h5 className="card-header" style={{ backgroundColor: "#efffed" }} >Szukaj</h5>
                <div className="card-body">
                    <form onSubmit={this.props.handleSearch}>
                        <div className="input-group">
                            <input type="text" className="form-control" name="query" value={this.props.query} onChange={this.props.handleOnChange} placeholder="Szukaj..."  />
                        <span className="input-group-btn">
                                <button className="btn btn-secondary" type="submit">{this.props.allMap === true ? "Dodaj do filtru!" : "Wyszukaj!" }</button>
                        </span>
                        </div>
                    </form>
                </div>
            </div>
        );
    }
}
export default SearchWidget;



