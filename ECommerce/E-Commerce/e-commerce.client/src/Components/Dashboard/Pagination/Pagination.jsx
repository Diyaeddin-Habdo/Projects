import React, { useEffect, useState } from 'react';
import ReactDOM from 'react-dom';
import ReactPaginate from 'react-paginate';
import "./pagination.css";
export default function PaginatedItems({ itemsPerPage,data ,setPage}) {

    const pageCount = data.length / itemsPerPage;

    return (
        <>
            <ReactPaginate
                breakLabel="..."
                nextLabel=">>"
                onPageChange={(e) => setPage(e.selected + 1)}
                pageRangeDisplayed={2}
                pageCount={pageCount}
                previousLabel="<<"
                renderOnZeroPageCount={null}
                containerClassName="custom-pagination d-flex align-items-center justify-content-end"
                pageLinkClassName="pagination-tag-anchor mx-2 text-secondary bg-primary rounded-circle"
                activeLinkClassName="bg-primary text-white"
            />
        </>
    );
}